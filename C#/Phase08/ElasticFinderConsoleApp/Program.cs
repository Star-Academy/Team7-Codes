using System.Threading.Tasks;
using Nest;

namespace ElasticFinderConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new App(new TerminalCommandReader()).Run();
        }
    }
}
