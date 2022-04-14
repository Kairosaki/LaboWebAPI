using LaboWebAPI.DTO.FournisseurDTO;

namespace LaboWebAPI.Services.FournisseurServices
{
    public interface IFournisseurService
    {
        IEnumerable<FournisseurIndexDTO> Lire(FournisseurSearchDTO dto);
        FournisseurIndexDTO? LireUn(long id);
        long Ajouter(FournisseurAddDTO dto);
        bool Supprimer(long id);
        bool Modifier(long id, FournisseurEditDTO dto);
    }
}
