using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class UposlenikService
    {
        private readonly IMongoCollection<Uposlenik> _UposlenikCollection;

        public UposlenikService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _UposlenikCollection = mongoDatabase.GetCollection<Uposlenik>(
                databaseSettings.Value.UposlenikCollectionName);
        }

        public async Task<List<Uposlenik>> GetAsync() =>
            await _UposlenikCollection.Find(_ => true).ToListAsync();

        public async Task<Uposlenik?> GetAsync(string id) =>
            await _UposlenikCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Uposlenik newUposlenik) =>
            await _UposlenikCollection.InsertOneAsync(newUposlenik);

        public async Task UpdateAsync(string id, Uposlenik updatedUposlenik) =>
            await _UposlenikCollection.ReplaceOneAsync(x => x.Id == id, updatedUposlenik);

        public async Task RemoveAsync(string id) =>
            await _UposlenikCollection.DeleteOneAsync(x => x.Id == id);
    }
}
