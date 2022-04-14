using LaboADO.Models;
using LaboADO.Repositories;
using LaboWebAPI.DTO.FournisseurDTO;
using LaboWebAPI.Exceptions;

namespace LaboWebAPI.Services.FournisseurServices
{
    public class FournisseurService : IFournisseurService
    {
        private readonly FournisseurRepository _fournisseurRepository;
        private readonly AdresseRepository _adresseRepository;

        public FournisseurService(FournisseurRepository fournisseurRepository, AdresseRepository adresseRepository)
        {
            _fournisseurRepository = fournisseurRepository;
            _adresseRepository = adresseRepository;
        }


        // TODO : ajouter l'adresse au moment de la création de fournisseur
        public long Ajouter(FournisseurAddDTO dto)
        {
            IEnumerable<Fournisseur> fournisseurs = _fournisseurRepository.FindAll();

            // Relation Fournisseur Adresse One To One
            try
            {
                if (fournisseurs.Any(f =>
                                f.Nom == dto.NomFournisseur
                                && f.Prenom == dto.PrenomFournisseur
                                && f.NumeroTelephone == dto.Telephone
                             )
                )
                {
                    throw new DuplicateAdresseException();
                }
            }
            catch (Exception)
            {
                return -1;
            }
            long adresseId = _adresseRepository.FindAll().Last().AdresseId + 1;
            long fournisseurId = fournisseurs.Last().FournisseurId + 1;
            try
            {
                if (dto.NomFournisseur != null && dto.PrenomFournisseur != null && dto.Telephone != null
                && dto.Fax != null && dto.Email != null && dto.Website != null
                )
                {
                    _fournisseurRepository.Add(
                        new Fournisseur()
                        {
                            FournisseurId = fournisseurId,
                            Nom = dto.NomFournisseur,
                            Prenom = dto.PrenomFournisseur,
                            NumeroTelephone = dto.Telephone,
                            NumeroFax = dto.Fax,
                            Email = dto.Email,
                            Website = dto.Website,
                            AdresseId = adresseId,
                        }
                    );

                    if (dto.Rue != null && dto.Ville != null && dto.Pays != null)
                    {
                        Adresse adresse = new Adresse()
                        {
                            AdresseId = adresseId,
                            Rue = dto.Rue,
                            Ville = dto.Ville,
                            Codepostal = dto.Codepostal,
                            Pays = dto.Pays,
                            FournisseurId = fournisseurId
                        };
                        _adresseRepository.Add(adresse, fournisseurId);
                        _fournisseurRepository.SetAdresseId(adresseId, fournisseurId);
                    }

                }
                else
                {
                    throw new MissingFieldException();
                }
            }
            catch(Exception)
            {
                return -1;
            }
            

            return _fournisseurRepository.FindAll().Last().FournisseurId;
        }

        public IEnumerable<FournisseurIndexDTO> Lire(FournisseurSearchDTO dto)
        {
            return _fournisseurRepository
                .GetWithFilters(dto.Keyword, dto.Codepostal, dto.Location, dto.Limit)
                .Select(f => new FournisseurIndexDTO
                {
                    Id = f.FournisseurId,
                    Nom = f.Nom,
                    Prenom = f.Prenom,
                    NumeroTelephone = f.NumeroTelephone,
                    NumeroFax = f.NumeroFax,
                    Email = f.Email,
                    Website = f.Website,
                    Adresse = f.Adresse
                });
        }

        public FournisseurIndexDTO? LireUn(long id)
        {
            try
            {
                Fournisseur fournisseur = _fournisseurRepository.FindOneById(id);
                return new FournisseurIndexDTO
                {
                    Id = fournisseur.FournisseurId,
                    Nom = fournisseur.Nom,
                    Prenom = fournisseur.Prenom,
                    NumeroTelephone = fournisseur.NumeroTelephone,
                    NumeroFax = fournisseur.NumeroFax,
                    Email = fournisseur.Email,
                    Website = fournisseur.Website,
                    Adresse = _adresseRepository.FindOneById(fournisseur.AdresseId)
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Modifier(long id, FournisseurEditDTO dto)
        {
            try
            {
                Fournisseur fournisseur = _fournisseurRepository.FindOneById(id);

                IEnumerable<Fournisseur> fournisseurs = _fournisseurRepository.FindAll();

                try
                {
                    if (fournisseurs
                        .Any(f =>
                            (f.Nom != null && f.Prenom != null && f.NumeroTelephone != null)
                            && (f.Nom == dto.Nom && f.Prenom == dto.Prenom && f.NumeroTelephone == dto.NumeroTelephone)
                            && f.FournisseurId != id
                        )
                   )
                    {
                        throw new UniqueFournisseurException();
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                Adresse? adresse = _adresseRepository.FindAll().FirstOrDefault(a => a.FournisseurId == fournisseur.FournisseurId);


                if (adresse != null)
                {
                    adresse.Numero = dto.Numero;
                    adresse.Rue = dto.Rue ?? adresse.Rue;
                    adresse.Ville = dto.Ville ?? adresse.Ville;
                    adresse.Codepostal = dto.Codepostal;
                    adresse.Pays = dto.Pays ?? adresse.Pays;
                    _adresseRepository.Edit(adresse);
                }

                _fournisseurRepository.Edit(fournisseur);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Supprimer(long id)
        {
            Fournisseur? fournisseur = _fournisseurRepository.FindOneById(id);
            long adresseId = _adresseRepository.FindAdresseIdByFournisseurId(id);
            if (fournisseur is null)
            {
                return false;
            }
            _adresseRepository.Remove(adresseId);
            _fournisseurRepository.Remove(id);                       
            return true;
        }
    }
}
