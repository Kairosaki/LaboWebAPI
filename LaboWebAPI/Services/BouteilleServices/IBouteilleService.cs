using LaboWebAPI.DTO.BouteilleDTO;

namespace LaboWebAPI.Services.BouteilleServices
{
    public interface IBouteilleService
    {
        IEnumerable<BouteilleIndexDTO> Lire(BouteilleSearchDTO dto);
        BouteilleIndexDTO? LireUn(long id);
        long Ajouter(BouteilleAddDTO dto);
        bool Supprimer(long id);
        bool Modifier(long id, BouteilleEditDTO dto);
    }
}
