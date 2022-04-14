using LaboADO.Models;

namespace LaboWebAPI.DTO.FournisseurDTO
{
    public class FournisseurIndexDTO
    {        
        public long Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? NumeroTelephone { get; set; }
        public string? NumeroFax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public Adresse? Adresse { get; set; }
    }
}
