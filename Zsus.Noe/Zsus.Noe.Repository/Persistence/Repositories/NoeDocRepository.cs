using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NServiceBus.Logging;
using Zsus.Noe.Repository.Domain.Entities;

namespace Zsus.Noe.Repository.Persistence.Repositories
{
    public class NoeDocRepository : BaseMongoRepository<NoeDoc>
    {
        static ILog log = LogManager.GetLogger<NoeDocRepository>();

        private const string NoeDocCollectionName = "Noe";

        private readonly MongoDataContext _dataContext;

        public NoeDocRepository(MongoDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<NoeDoc> Collection => 
            _dataContext.MongoDatabase.GetCollection<NoeDoc>(NoeDocCollectionName);

    }
}
