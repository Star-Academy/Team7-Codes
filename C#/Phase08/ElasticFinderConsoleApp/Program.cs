using System.Threading.Tasks;
using ElasticFinderConsoleApp.View;

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
