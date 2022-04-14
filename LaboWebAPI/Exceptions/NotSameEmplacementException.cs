namespace LaboWebAPI.Exceptions
{
    public class NotSameEmplacementException : Exception
    {
        public NotSameEmplacementException() : base("L'emplacement est déjà pris")
        {

        }
    }
}
