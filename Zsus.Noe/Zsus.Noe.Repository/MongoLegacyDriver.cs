using System;
using Zsus.Noe.Repository;
using Zsus.Noe.Repository.Domain.Entities;
using Zsus.Noe.Utilities;
using System.Linq.Expressions;
using Zsus.Noe.Repository.Persistence.Repositories;
using Zsus.Noe.Repository.Persistence;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

/// <summary>
/// !! THIS CLASS NEEDS TO DIE AND GO AWAY !!
/// 
/// I did not have time to fix mongo repository
/// so we are using old-timey non-di/injectable 
/// class...
/// 
/// </summary>
namespace Zsus.Noe.Repository
{

    public class MongoLegacyDriverFactory
    {
        private static MongoLegacyDriver _inst = null;

        public static MongoLegacyDriver GetMongoLegacyDriver()
        {
            if (_inst != null)
            {
                return _inst;
            }
            else
            {
                _inst = new MongoLegacyDriver();
                return _inst;
            }
        }
    }
    public class MongoLegacyDriver
    {
        private IMongoClient client;

        public MongoLegacyDriver()
        {
            //client = new MongoClient("mongodb://localhost:27017/NoePocDB");
            client = new MongoClient(Constants.ZSUS_NOE_DB_CONNECTION);
            
        }
        public async Task<NoeDoc> GetNoeDoc(int _noeid, string _sagaid)
        {
            var db = client.GetDatabase(Constants.ZSUS_NOE_DB_DATABASE);
            var doc = await db.GetCollection<NoeDoc>("noe").Find(x => x.noeid == _noeid && x.sagaid == _sagaid).ToListAsync();
            return doc[0];
        }

        public async Task UpdateNoeDoc(NoeDoc _noedoc)
        {
            var db = client.GetDatabase(Constants.ZSUS_NOE_DB_DATABASE);
            var collection = db.GetCollection<BsonDocument>("noe");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", _noedoc.Id);
            var update = Builders<BsonDocument>.Update
                .Set("description", _noedoc.description)
                .Set("timestamp", _noedoc.timestamp)
                .Set("needsapproval", _noedoc.needsapproval)
                .Set("approved", _noedoc.approved)
                .Set("needsnegotiation", _noedoc.needsnegotiation)
                .Set("negotiated", _noedoc.negotiated)
                .Set("needsoffering", _noedoc.needsoffering)
                .Set("offered", _noedoc.offered)
                .Set("status", _noedoc.status);
            var results = await collection.UpdateOneAsync(filter, update);
        }

    }
}
