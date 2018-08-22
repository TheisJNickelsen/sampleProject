using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SampleSolution.Data.Contexts.MongoDb.Models;

namespace SampleSolution.Data.Contexts.MongoDb
{
    public class UserStreamContext
    {
        private readonly IMongoDatabase _database = null;

        public UserStreamContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<UserStream> UserStream
        {
            get
            {
                return _database.GetCollection<UserStream>("UserStream");
            }
        }
    }
}
