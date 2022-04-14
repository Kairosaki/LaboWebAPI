using LaboADO.Models;
using LaboADO.Repositories;
using LaboWebAPI.DTO.AdresseDTO;
using LaboWebAPI.Exceptions;
using LaboWebAPI.Services.FournisseurServices;

namespace LaboWebAPI.Services.AdresseServices
{
    public class AdresseService : IAdresseService
    {
        private readonly AdresseRepository _adresseRepository;

        private readonly FournisseurRepository _fournisseurRepository;

        public AdresseService(AdresseRepository adresseRepository, FournisseurRepository fournisseurRepository)
        {
            _adresseRepository = adresseRepository;
            _fournisseurRepository = fournisseurRepository;
        }

        public IEnumerable<AdresseIndexDTO> Lire(AdresseSearchDTO dto)
        {
            IEnumerable<Adresse> adresses = _adresseRepository.FindAll();
            return _adresseRepository
                    .GetWithFilters(dto.Keyword, dto.Codepostal, dto.Limit)
                    .Select(a =>
                        new AdresseIndexDTO
                        {
                            Id = a.AdresseId,
                            Numero = a.Numero,
                            Rue = a.Rue,
                            Ville = a.Ville,
                            Codepostal = a.Codepostal,
                            Pays = a.Pays,
                            NomFournisseur = a.Fournisseur.Nom,
                            PrenomFournisseur = a.Fournisseur.Prenom
                        }
                      );
        }

        public AdresseIndexDTO? LireUn(long id)
        {            
            try
            {
                Adresse adresse = _adresseRepository.FindOneById(id);
                return new AdresseIndexDTO
                {
                    Id = adresse.AdresseId,
                    Numero = adresse.Numero,
                    Rue = adresse.Rue,
                    Ville = adresse.Ville,
                    Codepostal = adresse.Codepostal,
                    Pays = adresse.Pays,
                    NomFournisseur = adresse.Fournisseur.Nom,
                    PrenomFournisseur = adresse.Fournisseur.Prenom
                };
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public bool Modifier(long id, AdresseEditDTO dto)
        {
            Adresse? adresse = _adresseRepository.FindOneById(id);
            IEnumerable<Adresse> adresses = _adresseRepository.FindAll();
            if (adresse is null)
            {
                return false;
            }
            if (adresses
                    .Any(a =>
                        (a.Rue != null && a.Ville != null && a.Codepostal != 0 && a.Pays != null)
                        && (a.Numero == dto.Numero && a.Rue == dto.Rue && a.Ville == dto.Ville && a.Codepostal == dto.Codepostal && a.Pays == dto.Pays)
                        && a.AdresseId != id)
                    )
            {
                throw new UniqueAdresseException();
            }
            _adresseRepository.Edit(adresse);
            return true;
        }

    }
}
