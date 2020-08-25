namespace InvertedIndexEngine.CustomException
{
    public class ElasticClientErrorException : System.Exception
    {
        public ElasticClientErrorException(string message) : base(message)
        {
        }
    }
}