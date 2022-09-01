using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSEIbackendREST.Entities;

namespace WSEIbackendREST.Repositories
{
    public class MongoDbTrackRepository : ITrackRepository
    {
        private const string databaseName = "wsei";
        private const string collectionName = "employee";

        private readonly IMongoCollection<Track> itemsCollection;
        private readonly FilterDefinitionBuilder<Track> filterBuilder = Builders<Track>.Filter;
        
        public MongoDbTrackRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Track>(collectionName);
        }

        public async Task CreateItemAsync(Track item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task DeleteAllItemsAsync()
        {
            foreach (var item in itemsCollection.Find(new BsonDocument()).ToList())
            {
                var filter = filterBuilder.Eq(titem => titem.Id, item.Id);
                await itemsCollection.DeleteManyAsync(filter);
            }
        }

        public async Task<Track> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Track>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Track item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}