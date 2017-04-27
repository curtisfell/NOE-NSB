using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MongoDB.Driver;

namespace Zsus.Noe.Repository.Persistence
{
    public class MongoDataContext
    {
        public MongoDataContext()
            : this("MongoDbNoe")
        {
        }

        public MongoDataContext(string connectionName)
        {
            var url = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            var mongoUrl = new MongoUrl(url);
            IMongoClient client = new MongoClient(mongoUrl);
            MongoDatabase = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase MongoDatabase { get; }
    }
}
