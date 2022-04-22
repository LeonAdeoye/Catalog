using Catalog.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

string CONNECTION_URI = "mongodb://w**h_user:w**ch_user@leonadeoyemongodbcluster-shard-00-01-gni1u.azure.mongodb.net:27017,leonadeoyemongodbcluster-shard-00-00-gni1u.azure.mongodb.net:27017,leonadeoyemongodbcluster-shard-00-02-gni1u.azure.mongodb.net:27017/admin?serverSelectionTimeoutMS=20000&readPreference=primary&ssl=true";

var builder = WebApplication.CreateBuilder(args);
// Need to esnsure serialization in Mongo DB is string type for both GUID and DataTimeOffset.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
builder.Services.AddSingleton<IMongoClient>(serviceProvider => new MongoClient(CONNECTION_URI));
builder.Services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

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

app.Run();
