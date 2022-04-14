namespace LaboWebAPI.Exceptions
{
    public class DuplicateAdresseException : Exception
    {
        public DuplicateAdresseException() : base("l'adresse existe déjà !")
        {

        }
    }
}
