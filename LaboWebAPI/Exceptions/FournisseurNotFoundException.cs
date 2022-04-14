namespace LaboWebAPI.Exceptions
{
    public class FournisseurNotFoundException : Exception
    {
        public FournisseurNotFoundException() : base("Le fournisseur renseigné n'existe pas")
        {

        }
    }
}
