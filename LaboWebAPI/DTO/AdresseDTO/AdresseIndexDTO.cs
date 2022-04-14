namespace LaboWebAPI.DTO.AdresseDTO
{
    public class AdresseIndexDTO
    {
        public long Id { get; set; }
        public int Numero { get; set; }
        public string? Rue { get; set; }
        public int Codepostal { get; set; }
        public string? Ville { get; set; }
        public string? Pays { get; set; }
        public string? NomFournisseur { get; set; }
        public string? PrenomFournisseur { get; set; }
    }
}
