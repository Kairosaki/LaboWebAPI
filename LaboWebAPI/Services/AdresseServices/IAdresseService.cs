using LaboWebAPI.DTO.AdresseDTO;

namespace LaboWebAPI.Services.AdresseServices
{
    public interface IAdresseService
    {
        IEnumerable<AdresseIndexDTO> Lire(AdresseSearchDTO dto);
        AdresseIndexDTO? LireUn(long id);
        bool Modifier(long id, AdresseEditDTO dto);
    }
}
