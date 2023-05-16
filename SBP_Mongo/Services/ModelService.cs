using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SBP_Mongo.Models;

namespace SBP_Mongo.Services
{
    public class ModelService
    {
        private readonly IMongoCollection<Model> _modelCollection;

        public ModelService(
            IOptions<VPDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(
                databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                databaseSettings.Value.DatabaseName);

            _modelCollection = mongoDatabase.GetCollection<Model>(
                databaseSettings.Value.ModelCollectionName);
        }

        public async Task<List<Model>> GetAsync() =>
            await _modelCollection.Find(_ => true).ToListAsync();

        public async Task<Model?> GetAsync(string id) =>
            await _modelCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Model newModel) =>
            await _modelCollection.InsertOneAsync(newModel);

        public async Task UpdateAsync(string id, Model updatedModel) =>
            await _modelCollection.ReplaceOneAsync(x => x.Id == id, updatedModel);

        public async Task RemoveAsync(string id) =>
            await _modelCollection.DeleteOneAsync(x => x.Id == id);
    }
}
