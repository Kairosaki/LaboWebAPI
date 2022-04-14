
using Newtonsoft.Json;

namespace LaboConsoAPI.Models
{
    public class Emplacement
    {
        [JsonProperty("emplacement_id")]
        public long EmplacementId { get; set; }

        [JsonProperty("casier")]
        public string Casier { get; set; }

        [JsonProperty("etagere")]
        public string Etagere { get; set; }

        [JsonProperty("libre")]
        public bool Disponible { get; set; }

        public override string ToString()
        {
            string emplacement = $"Casier : {Casier}" +
                               $"\nEtagere : {Etagere}" +
                               $"\nLibre : {Disponible}"
                               ;
            return emplacement;
        }
    }
}
