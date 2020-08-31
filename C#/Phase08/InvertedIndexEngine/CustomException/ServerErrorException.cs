namespace InvertedIndexEngine.CustomException
{
    public class ServerErrorException : System.Exception
    {
        public ServerErrorException(string message) : base(message)
        {
        }
    }
}