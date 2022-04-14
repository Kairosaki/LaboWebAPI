namespace LaboWebAPI.Exceptions
{
    public class UniqueFournisseurException : Exception
    {
        public UniqueFournisseurException() : base("Le fournisseur doit être unique")
        {

        }
    }
}
