using Newtonsoft.Json;

namespace LaboConsoAPI.Models
{
    public class Bouteille
    {
        [JsonProperty("bouteille_id")]
        public long BouteilleId { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("degree")]
        public decimal DegreeAlcool { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("miseEnBouteille")]
        public int Annee { get; set; }

        [JsonProperty("marque")]
        public string Marque { get; set; }

        [JsonProperty("origine")]
        public string Origine { get; set; }

        [JsonProperty("pays")]
        public string Pays { get; set; }

        [JsonProperty("enStock")]
        public bool EnStock { get; set; }

        [JsonProperty("review")]
        public string? Review { get; set; }

        [JsonProperty("nomCompletFournisseur")]
        public string NomComplet { get; set; }

        [JsonProperty("nomEmplacement")]
        public string Emplacement { get; set; }

        private string estDisponible(bool enStock)
        {
            return enStock ? "Oui" : "Non";
        }

        public override string ToString()
        {
            string bouteille = $"Label : {Label}" +
                               $"\nType : {Type}" +
                               $"\nDegree : {DegreeAlcool}" +
                               $"\nVolume : {Volume}" +
                               $"\nMise en bouteille : {Annee}" +
                               $"\nMarque : {Marque}" +
                               $"\nOrigine : {Origine}" +
                               $"\nPays : {Pays}" +
                               $"\nEn stock : {EnStock}" +
                               $"\nReview : {Review}" +
                               $"\nNomComplet : {NomComplet}" +
                               $"\nEmplacement : {Emplacement}"
                               ;
            return bouteille;
        }
    }
}
