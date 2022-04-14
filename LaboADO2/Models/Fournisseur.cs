using LaboADO.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LaboADO.Models
{
    public class Fournisseur
    {
        private Adresse? _Adresse;
        public long FournisseurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NumeroTelephone { get; set; }
        public string? NumeroFax { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public long AdresseId { get; set; }

        [JsonIgnore]
        public Adresse? Adresse { get => _Adresse ?? new AdresseRepository().FindOneById(AdresseId); set => _Adresse = value; }
    }
}
