using LaboADO.Models;
using LaboADO.Repositories;
using LaboWebAPI.DTO.EmplacementDTO;
using LaboWebAPI.Exceptions;

namespace LaboWebAPI.Services.EmplacementServices
{
    public class EmplacementService : IEmplacementService
    {       

        private readonly EmplacementRepository _emplacementRepository;

        public EmplacementService(EmplacementRepository emplacementRepository)
        {
            _emplacementRepository = emplacementRepository;
        }

        public long Ajouter(EmplacementAddDTO dto)
        {
            IEnumerable<Emplacement> emplacements = _emplacementRepository.FindAll();

            try
            {
                if (emplacements.Any(e => e.Casier == dto.Casier && e.Etagere == dto.Etagere))
                {
                    throw new DuplicateEmplacementException();
                }
            }
            catch(Exception)
            {
                return -1;
            }

            Emplacement emplacement = new Emplacement()
                                        {
                                            EmplacementId = 0,
                                            Casier = dto.Casier ?? "",
                                            Etagere = dto.Etagere ?? "",
                                            Disponible = dto.Libre
                                        };

            if (dto.Etagere != null && dto.Casier != null)
            {
                _emplacementRepository.Add(emplacement);
            }
            else
            {
                throw new MissingFieldException();
            }

            return _emplacementRepository.FindAll().Last().EmplacementId;
        }

        public IEnumerable<EmplacementIndexDTO> Lire(EmplacementSearchDTO dto)
        {
            return _emplacementRepository.GetWithFilters(dto.Keyword, dto.Limit)
                     .Select(e =>
                        new EmplacementIndexDTO
                        {
                            Id = e.EmplacementId,
                            Casier = e.Casier,
                            Etagere = e.Etagere,
                            Libre = e.Disponible
                        }
                      );    
        }

        public EmplacementIndexDTO? LireUn(long id)
        {
            try
            {
                Emplacement emplacement = _emplacementRepository.FindOneById(id);

                return new EmplacementIndexDTO
                {
                    Id = emplacement.EmplacementId,
                    Casier = emplacement.Casier,
                    Etagere = emplacement.Etagere,
                    Libre = emplacement.Disponible
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Modifier(long id, EmplacementEditDTO dto)
        {
            Emplacement? emplacement = _emplacementRepository.FindOneById(id);
            IEnumerable<Emplacement> emplacements = _emplacementRepository.FindAll();

            if (emplacement is null)
            {
                return false;
            }

            if (emplacements
                    .Any(e =>
                        (e.Etagere != null && e.Casier != null)
                        && (e.Etagere == dto.Etagere && e.Casier == dto.Casier)
                        && e.EmplacementId != id)
                    )
            {
                throw new UniqueEmplacementException();
            }

            emplacement.Casier = dto.Casier ?? emplacement.Casier;
            emplacement.Etagere = dto.Etagere ?? emplacement.Etagere;
            emplacement.Disponible = dto.Libre;
            _emplacementRepository.Edit(id, emplacement);
            return true;
        }

        public bool ModifierPlace(long id)
        {
            try
            {
                Emplacement emplacement = _emplacementRepository.FindOneById(id);
                _emplacementRepository.ModifierPlace(id, !emplacement.Disponible);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool ModifierAllPlaces()
        {          
            _emplacementRepository.ModifierAllPlaces();
            return true;
        }

        public IEnumerable<EmplacementIndexDTO> GetAllLibres()
        {
            return _emplacementRepository.GetAllEmplacementsLibres()
                     .Select(e =>
                        new EmplacementIndexDTO
                        {
                            Id = e.EmplacementId,
                            Casier = e.Casier,
                            Etagere = e.Etagere,
                            Libre = e.Disponible
                        }
                      );
        }
    }
}
