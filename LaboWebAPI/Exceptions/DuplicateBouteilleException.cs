namespace LaboWebAPI.Exceptions
{
    public class DuplicateBouteilleException : Exception
    {
        public DuplicateBouteilleException() : base("la bouteille existe déjà !")
        {

        }
    }
}
