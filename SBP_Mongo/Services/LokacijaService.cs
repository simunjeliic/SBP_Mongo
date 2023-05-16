using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class LokacijaService
    {
        private readonly IMongoCollection<Lokacija> _modelCollection;

        public LokacijaService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _modelCollection = mongoDatabase.GetCollection<Lokacija>(
                databaseSettings.Value.LokacijaCollectionName);
        }

        public async Task<List<Lokacija>> GetAsync() =>
            await _modelCollection.Find(_ => true).ToListAsync();

        public async Task<Lokacija?> GetAsync(string id) =>
            await _modelCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Lokacija newLokacija) =>
            await _modelCollection.InsertOneAsync(newLokacija);

        public async Task UpdateAsync(string id, Lokacija updatedLokacija) =>
            await _modelCollection.ReplaceOneAsync(x => x.Id == id, updatedLokacija);

        public async Task RemoveAsync(string id) =>
            await _modelCollection.DeleteOneAsync(x => x.Id == id);
    }
}
