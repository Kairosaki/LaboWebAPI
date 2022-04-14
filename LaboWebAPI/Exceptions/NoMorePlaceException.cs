namespace LaboWebAPI.Exceptions
{
    public class NoMorePlaceException : Exception
    {
        public NoMorePlaceException() : base("Il n'y a plus d'emplacements libres !")
        {

        }
    }
}
