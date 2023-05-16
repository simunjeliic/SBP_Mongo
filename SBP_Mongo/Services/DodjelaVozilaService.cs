using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class DodjelaVozilaService
    {
        private readonly IMongoCollection<DodjelaVozila> _dodjelaCollection;

        public DodjelaVozilaService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _dodjelaCollection = mongoDatabase.GetCollection<DodjelaVozila>(
                databaseSettings.Value.DodjelaVozilaCollectionName);
        }

        public async Task<List<DodjelaVozila>> GetAsync() =>
            await _dodjelaCollection.Find(_ => true).ToListAsync();

        public async Task<DodjelaVozila?> GetAsync(string id) =>
            await _dodjelaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DodjelaVozila newDodjelaVozila) =>
            await _dodjelaCollection.InsertOneAsync(newDodjelaVozila);

        public async Task UpdateAsync(string id, DodjelaVozila updatedDodjelaVozila) =>
            await _dodjelaCollection.ReplaceOneAsync(x => x.Id == id, updatedDodjelaVozila);

        public async Task RemoveAsync(string id) =>
            await _dodjelaCollection.DeleteOneAsync(x => x.Id == id);
    }
}
