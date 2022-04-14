namespace LaboWebAPI.DTO.FournisseurDTO
{
    public class FournisseurSearchDTO
    {
        public string? Keyword { get; set; }

        public int Codepostal { get; set; }
        
        public string? Location { get; set; }

        public int Limit { get; set; } = 20;
    }
}
