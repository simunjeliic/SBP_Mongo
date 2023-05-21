using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SBP_Mongo.Models
{
    public class DodjelaVozila
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        public string Uposlenik { get; set; } = null!;
        public Uposlenik UposlenikO { get; set; } = null!;


        public string Vozilo { get; set; } = null!;
        public Vozilo VoziloO { get; set; } = null!;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Dodjeljeno { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? VratitiDo { get; set; }


    }
}
