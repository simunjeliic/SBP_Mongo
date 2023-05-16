using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class MarkaService
    {
        private readonly IMongoCollection<Marka> _markaCollection;

        public MarkaService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _markaCollection = mongoDatabase.GetCollection<Marka>(
                databaseSettings.Value.MarkaCollectionName);
        }

        public async Task<List<Marka>> GetAsync() =>
            await _markaCollection.Find(_ => true).ToListAsync();

        public async Task<Marka?> GetAsync(string id) =>
            await _markaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Marka newMarka) =>
            await _markaCollection.InsertOneAsync(newMarka);

        public async Task UpdateAsync(string id, Marka updatedMarka) =>
            await _markaCollection.ReplaceOneAsync(x => x.Id == id, updatedMarka);

        public async Task RemoveAsync(string id) =>
            await _markaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
