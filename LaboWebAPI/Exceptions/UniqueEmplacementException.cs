namespace LaboWebAPI.Exceptions
{
    public class UniqueEmplacementException : Exception
    {
        public UniqueEmplacementException() : base("L'emplacement doit être unique !")
        {

        }
    }
}
