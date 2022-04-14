using LaboADO.Models;
using LaboADO.Repositories;
using System.Text.Json.Serialization;

namespace LaboADO.Models
{
    public class Bouteille
    {
        private Fournisseur _Fournisseur;

        private Emplacement _Emplacement;
        public long BouteilleId { get; set; }
        public string Label { get; set; }
        public WineType Type { get; set; }
        public decimal DegreeAlcool { get; set; }
        public decimal Volume { get; set; }
        public DateTime Date { get; set; }
        public string Marque { get; set; }
        public string Origine { get; set; }
        public string Pays { get; set; }
        public bool EnStock { get; set; }
        public string? Review { get; set; }
        public long FournisseurId { get; set; }        
        public long EmplacementId { get; set; }

        [JsonIgnore]
        public Fournisseur Fournisseur { get => _Fournisseur ?? new FournisseurRepository().FindOneById(FournisseurId); set => _Fournisseur = value; }

        [JsonIgnore]
        public Emplacement Emplacement { get => _Emplacement ?? new EmplacementRepository().FindOneById(EmplacementId); set => _Emplacement = value; }
    }
}
