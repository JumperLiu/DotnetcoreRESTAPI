using System;
using System.Collections.Generic;
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

        public void CreateMobilePhone(MobilePhone mobilePhone) => collection.InsertOne(mobilePhone);

        public void DeleteMobilePhone(Guid id) => collection.DeleteOne(filterBuilder.Eq(i => i.Id, id));

        public MobilePhone GetMobilePhone(Guid id) => collection.Find(i => i.Id == id).SingleOrDefault();

        public IEnumerable<MobilePhone> GetMobilePhones() => collection.Find(new BsonDocument()).ToEnumerable();

        public void UpdateMobilePhone(MobilePhone mobilePhone) => collection.ReplaceOne(filterBuilder.Eq(i => i.Id, mobilePhone.Id), mobilePhone);
    }
}