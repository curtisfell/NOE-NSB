using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zsus.Noe.Repository;
using Zsus.Noe.Repository.Domain.Entities;
using System.Linq.Expressions;
using Zsus.Noe.Repository.Persistence.Repositories;
using Zsus.Noe.Repository.Persistence;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Zsus.Noe.Utilities;

namespace Zsus.Noe.Test
{
    [TestClass]
    public class UnitTestRepository
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            string _sagaid = "bbf7798a-eacf-4ecf-ad69-2f246eac8c90";
            int _noeid = 2000;
                        
            // connect
            IMongoClient client = new MongoClient("mongodb://localhost:27017/NoePocDB");
            var db = client.GetDatabase("NoePocDB");
            //var collection = db.GetCollection<NoeDoc>("noe");
            
            // select / find
            var docs3 = await db.GetCollection<NoeDoc>("noe").Find(x => x.noeid == _noeid && x.sagaid == _sagaid).ToListAsync();
            
            // update
            var collection2 = db.GetCollection<BsonDocument>("noe");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", docs3[0].Id);
            var update = Builders<BsonDocument>.Update.Set("needsapproval", -1);

            // results of update
            var results = await collection2.UpdateOneAsync(filter, update);
            
            Assert.IsTrue(docs3.Count > 0);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string _sagaid = "bbf7798a-eacf-4ecf-ad69-2f246eac8c90";
            int _noeid = 2000;

            EmailNotifcationFactory
                    .GetEmailNotifcation()
                    .SendNeedsApproval(new EmailParameters(_noeid, Guid.Parse(_sagaid)));

            Assert.IsFalse(false);
        }

    }
}
