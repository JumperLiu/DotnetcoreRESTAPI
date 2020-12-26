using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetCoreRESTAPI.Models;
using DotnetCoreRESTAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetCoreRESTAPI.Repositories
{
    public class MongoDBRepository : IRepository
    {
        private const string DatabaseName = "EleticDB";
        private const string CollectionName = "MobilePhones";
        private IMongoCollection<MobilePhone> collection;
        private FilterDefinitionBuilder<MobilePhone> filterBuilder = Builders<MobilePhone>.Filter;

        public MongoDBRepository(IMongoClient mongoClient)
        => collection = mongoClient.GetDatabase(DatabaseName).GetCollection<MobilePhone>(CollectionName);

        public async Task CreateMobilePhoneAsync(MobilePhone mobilePhone) => await collection.InsertOneAsync(mobilePhone);

        public async Task DeleteMobilePhoneAsync(Guid id) => await collection.DeleteOneAsync(filterBuilder.Eq(i => i.Id, id));
        public async Task<MobilePhone> GetMobilePhoneAsync(Guid id) => await collection.Find(i => i.Id == id).SingleOrDefaultAsync();

        public async Task<IEnumerable<MobilePhone>> GetMobilePhonesAsync() => await collection.Find(new BsonDocument()).ToListAsync();

        public async Task UpdateMobilePhoneAsync(MobilePhone mobilePhone)
        => await collection.ReplaceOneAsync(filterBuilder.Eq(i => i.Id, mobilePhone.Id), mobilePhone);
    }
}