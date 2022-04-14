using LaboADO.Models;
using LaboWebAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace LaboWebAPI.DTO.BouteilleDTO
{
    public class BouteilleSearchDTO
    {
        public string? Keyword { get; set; }

        public string? Location { get; set; }

        [NotAfterThisYear]
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public decimal MinAlcool { get; set; }
        public decimal MaxAlcool { get; set; }
        public decimal MinVolume { get; set; }
        public decimal MaxVolume { get; set; }
        public byte Enstock { get; set; }

        [Range(2, 100)]
        public int Limit { get; set; } = 20;
        public List<WineType>? Types { get; set; } = new List<WineType>();
    }
}
