namespace LaboWebAPI.DTO.BouteilleDTO
{
    public class BouteilleIndexDTO
    {        
        public long Id { get; set; }
        public string? Label { get; set; }
        public string? Type { get; set; }
        public decimal Degree { get; set; }
        public decimal Volume { get; set; }
        public int MiseEnBouteille { get; set; }
        public string? Marque { get; set; }
        public string? Origine { get; set; }
        public string? Pays { get; set; }

        public bool EnStock { get; set; }
        public string? Review { get; set; }
        public string? NomFournisseur { get; set; }
        public string? PrenomFournisseur { get; set; }
        public string NomCompletFournisseur
        {
            get
            {
                return NomFournisseur + ' ' + PrenomFournisseur;
            }
        }

        public string? Casier { get; set; }
        public string? Etagere { get; set; }

        public string NomEmplacement
        {
            get
            {
                return Casier + ' ' + Etagere;
            }
        }
    }
}
