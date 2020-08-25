namespace InvertedIndexEngine.CustomException
{
    public class RequestNotSentException : System.Exception
    {
        public RequestNotSentException() : this("Problem sendig request. request was not sent.")
        {
        }

        public RequestNotSentException(string message) : base(message)
        {
        }
    }
}