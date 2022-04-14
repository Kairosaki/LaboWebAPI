using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.EmplacementDTO
{
    public class EmplacementEditDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? Casier { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string? Etagere { get; set; }

        [Required]
        [Range(0,1)]
        public bool Libre { get; set; }
    }
}
