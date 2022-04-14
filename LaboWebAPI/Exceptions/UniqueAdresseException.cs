namespace LaboWebAPI.Exceptions
{
    public class UniqueAdresseException : Exception
    {
        public UniqueAdresseException() : base("L'adresse doit être unique")
        {

        }
    }
}
