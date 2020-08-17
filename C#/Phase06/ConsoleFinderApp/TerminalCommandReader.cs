using System;

namespace ConsoleFinderApp
{
    public class TerminalCommandReader : ICommandReader
    {
        public string ReadCommand()
        {
            return Console.ReadLine();
        }

        public void SendResponse(string response)
        {
            Console.WriteLine(response);
        }
    }
}