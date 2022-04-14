namespace LaboWebAPI.Exceptions
{
    public class DuplicateEmplacementException : Exception
    {
        public DuplicateEmplacementException() : base("l'emplacement existe déjà !")
        {

        }
    }
}
