namespace LaboWebAPI.DTO.EmplacementDTO
{
    public class EmplacementSearchDTO
    {
        public string? Keyword { get; set; }

        public int Limit { get; set; } = 20;
    }
}
