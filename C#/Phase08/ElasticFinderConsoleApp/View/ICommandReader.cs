namespace ElasticFinderConsoleApp.View
{
    public interface ICommandReader
    {
        string ReadCommand();

        void SendResponse(string response);
    }
}