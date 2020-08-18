namespace ConsoleFinderApp
{
    public interface ICommandReader
    {
        string ReadCommand();

        void SendResponse(string response);
    }
}