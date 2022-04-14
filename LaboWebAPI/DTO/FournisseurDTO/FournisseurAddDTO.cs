using LaboADO.Models;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.FournisseurDTO
{
    public class FournisseurAddDTO
    {
        [Required]
        public string? NomFournisseur { get; set; }

        [Required]
        public string? PrenomFournisseur { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        public string? Fax { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Website { get; set; }
        public int Numero { get; set; }

        [Required]
        public string? Rue { get; set; }

        [Required]
        public string? Ville { get; set; }
        public int Codepostal { get; set; }

        [Required]
        public string? Pays { get; set; }
    }
}
