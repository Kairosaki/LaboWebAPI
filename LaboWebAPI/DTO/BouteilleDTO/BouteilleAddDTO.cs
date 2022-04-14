using LaboADO.Models;
using LaboWebAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.BouteilleDTO
{
    public class BouteilleAddDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? Label { get; set; }

        [Required]
        public WineType Type { get; set; }

        [Required]
        public decimal DegreeAlcool { get; set; }

        [Required]
        [Range(0,30)]
        public decimal Volume { get; set; }

        [Required]
        [NotAfterToday]
        public DateTime Date { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string? Marque { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string? Origine { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string? Pays { get; set; }

        [Required]
        public bool EnStock { get; set; }

        [MinLength(4)]
        [MaxLength(50)]
        public string? Review { get; set; }

        [Required]
        public string? NomFournisseur { get; set; }

        [Required]
        public string? PrenomFournisseur { get; set; }

        [Required]
        public string? NumeroTelephone { get; set; }
    }
}
