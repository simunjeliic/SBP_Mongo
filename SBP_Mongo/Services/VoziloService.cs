using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class VoziloService
    {
        private readonly IMongoCollection<Vozilo> _voziloCollection;

        public VoziloService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _voziloCollection = mongoDatabase.GetCollection<Vozilo>(
                databaseSettings.Value.VoziloCollectionName);
        }

        public async Task<List<Vozilo>> GetAsync() =>
            await _voziloCollection.Find(_ => true).ToListAsync();

        public async Task<Vozilo?> GetAsync(string id) =>
            await _voziloCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vozilo newVozilo) =>
            await _voziloCollection.InsertOneAsync(newVozilo);

        public async Task UpdateAsync(string id, Vozilo updatedVozilo) =>
            await _voziloCollection.ReplaceOneAsync(x => x.Id == id, updatedVozilo);

        public async Task RemoveAsync(string id) =>
            await _voziloCollection.DeleteOneAsync(x => x.Id == id);
    }
}
