using LaboADO.Repositories;
using System.Text.Json.Serialization;

namespace LaboADO.Models
{
    public class Adresse
    {
        private Fournisseur _Fournisseur;
        public long AdresseId { get; set; }
        public int Numero { get; set; }
        public string Rue { get; set; }
        public string Ville { get; set; }
        public int Codepostal { get; set; }
        public string Pays { get; set; }
        public long FournisseurId { get; set; }

        [JsonIgnore]
        public Fournisseur Fournisseur { 
            get => _Fournisseur = _Fournisseur ?? new FournisseurRepository().FindOneById(FournisseurId); 
        }
    }
}
