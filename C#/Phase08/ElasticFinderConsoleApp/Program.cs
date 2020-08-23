using System;
using Nest;

namespace ElasticFinderConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new App(new TerminalCommandReader()).Run();
        }
    }
}
