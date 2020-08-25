namespace InvertedIndexEngine.CustomException
{
    public class ElasticServerErrorException : System.Exception
    {
        public ElasticServerErrorException(string message) : base(message)
        {
        }
    }
}