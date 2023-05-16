using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class VrstaService
    {
        private readonly IMongoCollection<Vrsta> _vrstaCollection;

        public VrstaService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _vrstaCollection = mongoDatabase.GetCollection<Vrsta>(
                databaseSettings.Value.VrstaCollectionName);
        }

        public async Task<List<Vrsta>> GetAsync() =>
            await _vrstaCollection.Find(_ => true).ToListAsync();

        public async Task<Vrsta?> GetAsync(string id) =>
            await _vrstaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vrsta newVrsta) =>
            await _vrstaCollection.InsertOneAsync(newVrsta);

        public async Task UpdateAsync(string id, Vrsta updatedVrsta) =>
            await _vrstaCollection.ReplaceOneAsync(x => x.Id == id, updatedVrsta);

        public async Task RemoveAsync(string id) =>
            await _vrstaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
