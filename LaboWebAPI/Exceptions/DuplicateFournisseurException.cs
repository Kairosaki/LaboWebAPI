namespace LaboWebAPI.Exceptions
{
    public class DuplicateFournisseurException : Exception
    {
        public DuplicateFournisseurException() : base("le fournisseur existe déjà !")
        {

        }
    }
}
