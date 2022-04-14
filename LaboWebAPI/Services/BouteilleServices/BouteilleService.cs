using LaboADO.Models;
using LaboADO.Repositories;
using LaboWebAPI.DTO.BouteilleDTO;
using LaboWebAPI.Exceptions;

namespace LaboWebAPI.Services.BouteilleServices
{
    public class BouteilleService : IBouteilleService
    {
        private readonly BouteilleRepository _bouteilleRepository;

        private readonly FournisseurRepository _fournisseurRepository;

        private readonly EmplacementRepository _emplacementRepository;

        public BouteilleService(BouteilleRepository bouteilleRepository, EmplacementRepository emplacementRepository, FournisseurRepository fournisseurRepository)
        {
            _bouteilleRepository = bouteilleRepository;
            _emplacementRepository = emplacementRepository;
            _fournisseurRepository = fournisseurRepository;
        }

        public IEnumerable<BouteilleIndexDTO> Lire(BouteilleSearchDTO dto)
        {
            IEnumerable<Bouteille> bouteilles = _bouteilleRepository.FindAll();
            return _bouteilleRepository
                .GetWithFilters(dto.Keyword, dto.Location, dto.MinYear, dto.MaxYear, dto.MinAlcool, dto.MaxAlcool, dto.MinVolume, dto.MaxVolume, dto.Types, dto.Enstock, dto.Limit)
                .Select(b =>
                    new BouteilleIndexDTO()
                    {
                        Id = b.BouteilleId,
                        Label = b.Label,
                        Type = b.Type.ToString(),
                        Degree = b.DegreeAlcool,
                        Volume = b.Volume,
                        MiseEnBouteille = b.Date.Year,
                        Marque = b.Marque,
                        Origine = b.Origine,
                        Pays = b.Pays,
                        EnStock = b.EnStock,
                        Review = b.Review,
                        NomFournisseur = b.Fournisseur.Nom,
                        PrenomFournisseur = b.Fournisseur.Prenom,
                        Casier = b.Emplacement.Casier,
                        Etagere = b.Emplacement.Etagere
                    }
                );
        }

        public BouteilleIndexDTO? LireUn(long id)
        {
            try
            {
                Bouteille bouteille = _bouteilleRepository.FindOneById(id);
                return new BouteilleIndexDTO
                {
                    Id = bouteille.BouteilleId,
                    Label = bouteille.Label,
                    Type = bouteille.Type.ToString(),
                    Degree = bouteille.DegreeAlcool,
                    Volume = bouteille.Volume,
                    MiseEnBouteille = bouteille.Date.Year,
                    Marque = bouteille.Marque,
                    Origine = bouteille.Origine,
                    Pays = bouteille.Pays,
                    EnStock = bouteille.EnStock,
                    Review = bouteille.Review ?? "",
                    NomFournisseur = bouteille.Fournisseur.Nom,
                    PrenomFournisseur = bouteille.Fournisseur.Prenom,
                    Casier = bouteille.Emplacement.Casier,
                    Etagere = bouteille.Emplacement.Etagere
                };
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        public long Ajouter(BouteilleAddDTO dto)
        {
            try
            {
                IEnumerable<Bouteille> bouteilles = _bouteilleRepository.FindAll();

                if (bouteilles.FirstOrDefault(b =>
                        b.Fournisseur.Nom == dto.NomFournisseur
                        && b.Fournisseur.Prenom == dto.PrenomFournisseur
                        && b.Fournisseur.NumeroTelephone == dto.NumeroTelephone
                        ) == null)
                {
                    throw new FournisseurNotFoundException();
                }

                Emplacement? emplacement = _emplacementRepository.FindAll().FirstOrDefault(e => e.Disponible);

                if (emplacement is null)
                {
                    throw new NoMorePlaceException();
                }

                Fournisseur fournisseur = _fournisseurRepository.FindAll().First(f => f.Nom == dto.NomFournisseur && f.Prenom == dto.PrenomFournisseur && f.NumeroTelephone == dto.NumeroTelephone);

                Bouteille? bouteille = new Bouteille
                {
                    BouteilleId = bouteilles.Last().BouteilleId + 1,
                    Label = dto.Label ?? "",
                    Type = dto.Type,
                    DegreeAlcool = dto.DegreeAlcool,
                    Volume = dto.Volume,
                    Date = dto.Date,
                    Marque = dto.Marque ?? "",
                    Origine = dto.Origine ?? "",
                    Pays = dto.Pays ?? "",
                    EnStock = dto.EnStock,
                    Review = dto.Review,
                    FournisseurId = fournisseur.FournisseurId,
                    EmplacementId = emplacement.EmplacementId,
                    Fournisseur = fournisseur,
                    Emplacement = emplacement
                };

                if (bouteille is null)
                {
                    return -1;
                }

                _bouteilleRepository.Add(bouteille);

                return bouteille.BouteilleId;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public bool Modifier(long id, BouteilleEditDTO dto)
        {
            try
            {
                Bouteille bouteille = _bouteilleRepository.FindOneById(id);

                IEnumerable<Bouteille> bouteilles = _bouteilleRepository.FindAll();

                Emplacement? emplacement = _emplacementRepository.FindAll().FirstOrDefault(e => e.Disponible);

                if (emplacement is null)
                {
                    throw new NoMorePlaceException();
                }

                Fournisseur? fournisseur = _fournisseurRepository.FindAll()
                                            .FirstOrDefault(f => f.Nom == dto.NomFournisseur && f.Prenom == dto.PrenomFournisseur);

                if (fournisseur is null)
                {
                    return false;
                }

                Enum.TryParse(typeof(WineType), dto.Type, true, out object? winType);

                if (winType is null)
                {
                    return false;
                }

                bouteille.Label = dto.Label ?? bouteille.Label;
                bouteille.Type = (WineType) winType;
                bouteille.DegreeAlcool = dto.DegreeAlcool;
                bouteille.Volume = dto.Volume;
                bouteille.Date = dto.Date;
                bouteille.Marque = dto.Marque ?? bouteille.Marque;
                bouteille.Origine = dto.Origine ?? bouteille.Origine;
                bouteille.Pays = dto.Pays ?? bouteille.Pays;
                bouteille.EnStock = dto.EnStock;
                bouteille.Review = dto.Review;
                bouteille.FournisseurId = fournisseur.FournisseurId;
                bouteille.EmplacementId = emplacement.EmplacementId;
                bouteille.Fournisseur = fournisseur;
                bouteille.Emplacement = emplacement;

                emplacement.Disponible = !bouteille.EnStock;

                _emplacementRepository.Edit(emplacement.EmplacementId, emplacement);
                _bouteilleRepository.Edit(bouteille);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Supprimer(long id)
        {
            try
            {
                Bouteille bouteille = _bouteilleRepository.FindOneById(id);
                _bouteilleRepository.EditStock(bouteille);
                _emplacementRepository.ModifierPlace(bouteille.EmplacementId, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
