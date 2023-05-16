using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SBP_Mongo.Models
{
    public class Uposlenik
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Ime { get; set; } = null!;

        public string Prezime { get; set; } = null!;

        public string Pozicija { get; set; } = null!;

        public string Adresa { get; set; } = null!;

        public string BrojMobitela { get; set; } = null!;

        public string Jmbg { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
