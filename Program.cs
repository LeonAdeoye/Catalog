using Catalog.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

string CONNECTION_URI = "mongodb://workbench_user:workbench_user@leonadeoyemongodbcluster-shard-00-01-gni1u.azure.mongodb.net:27017,leonadeoyemongodbcluster-shard-00-00-gni1u.azure.mongodb.net:27017,leonadeoyemongodbcluster-shard-00-02-gni1u.azure.mongodb.net:27017/admin?serverSelectionTimeoutMS=20000&readPreference=primary&ssl=true";

var builder = WebApplication.CreateBuilder(args);
// Need to esnsure serialization in Mongo DB is string type for both GUID and DataTimeOffset.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
builder.Services.AddSingleton<IMongoClient>(serviceProvider => new MongoClient(CONNECTION_URI));
builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

// Added to do health checks on https://localhost:7071/health
// You will also need a NugGet package: AspNetCore.HealthChecks.MongoDb to add a Mongo DB health check
builder.Services.AddHealthChecks()
    .AddMongoDb(CONNECTION_URI, 
        name: "mongodb", 
        timeout: TimeSpan.FromSeconds(20),
        tags: new[] { "ready" });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Also needed for healthchecks.
app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = (check) => check.Tags.Contains("ready")}); ;
app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = (_) => false }); ;


app.Run();
