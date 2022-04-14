using LaboWebAPI.DTO.EmplacementDTO;

namespace LaboWebAPI.Services.EmplacementServices
{
    public interface IEmplacementService
    {
        IEnumerable<EmplacementIndexDTO> Lire(EmplacementSearchDTO dto);
        EmplacementIndexDTO? LireUn(long id);
        long Ajouter(EmplacementAddDTO dto);
        bool ModifierPlace(long id);
        bool ModifierAllPlaces();
        bool Modifier(long id, EmplacementEditDTO dto);
    }
}
