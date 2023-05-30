using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;


namespace SBP_Mongo.Models
{
    public class Vozilo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string ModelVozila { get; set; } = null!;
        public string BrojSasije { get; set; } = null!;
        public string RegistracijskaOznaka { get; set; } = null!;
        public string GodinaProizvodnje { get; set; } = null!;
        public string VrstaVozila { get; set; } = null!;
        public string IdLokacije { get; set; } = null!;
        public string Gorivo { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;

        [NotMapped]
        public IFormFile PictureFile { get; set; } = null!;




    }
}
