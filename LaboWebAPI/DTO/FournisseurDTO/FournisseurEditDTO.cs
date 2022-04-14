using LaboADO.Models;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.FournisseurDTO
{
    public class FournisseurEditDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Nom { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Prenom { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(12)]
        public string? NumeroTelephone { get; set; }
        public string? NumeroFax { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Website { get; set; }
        public int Numero { get; set; }
        public string? Rue { get; set; }
        public string? Ville { get; set; }
        public int Codepostal { get; set; }
        public string? Pays { get; set; }
    }
}
