namespace SBP_Mongo.Models
{
    public class VPDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PozicijaCollectionName { get; set; } = null!;
    }
}
