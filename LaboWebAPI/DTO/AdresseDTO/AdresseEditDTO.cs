using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.AdresseDTO
{
    public class AdresseEditDTO
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
        [MinLength(2)]
        [MaxLength(10)]
        public int Codepostal { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Pays { get; set; }
    }
}
