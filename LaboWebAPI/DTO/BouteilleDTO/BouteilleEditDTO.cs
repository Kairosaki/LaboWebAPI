using LaboADO.Models;
using LaboWebAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.BouteilleDTO
{
    public class BouteilleEditDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Label { get; set; }

        [Required]
        //[OnlyFromWineType]
        public string? Type { get; set; }

        [Required]
        public decimal DegreeAlcool { get; set; }

        [Required]
        [Range(0,30)]
        public decimal Volume { get; set; }

        [Required]
        [NotAfterToday]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Marque { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Origine { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Pays { get; set; }

        [Required]
        [Range(0,1)]
        public bool EnStock { get; set; }

        public string? Review { get; set; }

        public string? NomFournisseur { get; set; }

        public string? PrenomFournisseur { get; set; }

    }
}
