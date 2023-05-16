namespace SBP_Mongo.Models
{
    public class VPDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string PozicijaCollectionName { get; set; } = null!;
        public string MarkaCollectionName { get; set; } = null!;
        public string ModelCollectionName { get; set; } = null!;
        public string LokacijaCollectionName { get; set; } = null!;
        public string VrstaCollectionName { get; set; } = null!;
        public string VoziloCollectionName { get; set; } = null!;
        public string UposlenikCollectionName { get; set; } = null!;
        public string DodjelaVozilaCollectionName { get; set; } = null!;

    }
}
