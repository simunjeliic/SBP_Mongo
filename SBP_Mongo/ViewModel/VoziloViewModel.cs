using System.ComponentModel.DataAnnotations.Schema;

namespace SBP_Mongo.ViewModel
{
    public class VoziloViewModel
    {
        public string Id { get; set; }
        public string ModelVozila { get; set; }
        public string BrojSasije { get; set; }
        public string RegistracijskaOznaka { get; set; }
        public string GodinaProizvodnje { get; set; }
        public string VrstaVozila { get; set; }
        public string IdLokacije { get; set; }
        public string Gorivo { get; set; }

        [NotMapped]
        public IFormFile PictureFile { get; set; }

        public string PictureUrl { get; set; }
    }
}
