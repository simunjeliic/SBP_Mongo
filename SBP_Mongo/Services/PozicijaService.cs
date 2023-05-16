using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class PozicijaService
    {
        private readonly IMongoCollection<Pozicija> _pozicijaCollection;

        public PozicijaService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _pozicijaCollection = mongoDatabase.GetCollection<Pozicija>(
                databaseSettings.Value.PozicijaCollectionName);
        }

        public async Task<List<Pozicija>> GetAsync() =>
            await _pozicijaCollection.Find(_ => true).ToListAsync();

        public async Task<Pozicija?> GetAsync(string id) =>
            await _pozicijaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pozicija newPozicija) =>
            await _pozicijaCollection.InsertOneAsync(newPozicija);

        public async Task UpdateAsync(string id, Pozicija updatedPozicija) =>
            await _pozicijaCollection.ReplaceOneAsync(x => x.Id == id, updatedPozicija);

        public async Task RemoveAsync(string id) =>
            await _pozicijaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
