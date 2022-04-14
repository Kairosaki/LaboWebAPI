using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.EmplacementDTO
{
    public class EmplacementAddDTO
    {
        [Required]
        public string? Casier { get; set; }

        [Required]
        public string? Etagere { get; set; }

        [Required]
        public bool Libre { get; set; }
    }
}
