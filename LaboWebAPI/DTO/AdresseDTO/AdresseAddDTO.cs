using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.AdresseDTO
{
    public class AdresseAddDTO
    {
        [Required]
        public int Numero { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string? Rue { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string? Ville { get; set; }

        [Required]
        [Range(2,10)]
        public int Codepostal { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Pays { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? NomFournisseur { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? PrenomFournisseur { get; set; }

        [Required]
        [MinLength(12)]
        [MaxLength(12)]
        public string? TelephoneFournisseur { get; set; }
    }
}
