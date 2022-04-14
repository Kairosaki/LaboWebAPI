namespace LaboWebAPI.DTO.AdresseDTO
{
    public class AdresseSearchDTO
    {
        public string? Keyword { get; set; }

        public int Codepostal { get; set; }

        public int Limit { get; set; } = 20;
    }
}
