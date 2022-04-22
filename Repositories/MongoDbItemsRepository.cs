using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {        
        private readonly static string DATABASE_NAME = "catalog";
        private readonly static string COLLECTION_NAME = "items";
        private readonly IMongoCollection<Item> itemsCollection;
        // This is another way of doing filtering compared to the one used by Tim Corey example.
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME);
            itemsCollection = database.GetCollection<Item>(COLLECTION_NAME);
        }
        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            var item = itemsCollection.Find(filter).SingleOrDefault();
            return item;
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
            itemsCollection.DeleteOne(filter);
        }
    }
}
