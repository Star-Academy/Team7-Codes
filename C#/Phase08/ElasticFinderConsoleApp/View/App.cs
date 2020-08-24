using System.IO;
using System.Threading.Tasks;
using ElasticFinderConsoleApp.ElasticCumminucation;
using ElasticFinderConsoleApp.Model;

namespace ElasticFinderConsoleApp.View
{
    public class App
    {
        private readonly ICommandReader commandReader;
        private const string IndexName = "inverte_index_using_elasticsearch";

        public App(ICommandReader commandReader)
        {
            this.commandReader = commandReader;
        }

        public async Task Run()
        {
            var path = GetPath();
            var documentIndexer = new DocumentIndexer(IndexName);
            await documentIndexer.DeleteIndex();
            await documentIndexer.CreateIndex();
            await documentIndexer.IndexDocuments(new FileReader(path));

            await CommandHandler(new QueryHandler(IndexName));
        }

        private async Task CommandHandler(QueryHandler queryHandler)
        {
            string command;
            while (!(command = GetQuery()).Equals("exit"))
            {
                var searchQuery = new SearchQuery(command);
                if (command.Trim().Length == 0)
                    continue;
                var result = await queryHandler.Find(searchQuery);
                commandReader.SendResponse(result.Count + " results found :");
                foreach (var item in result)
                {
                    commandReader.SendResponse("\t" + item.Name);
                }
            }
        }

        private string GetPath()
        {
            commandReader.SendResponse("Enter a directory path :");
            var path = commandReader.ReadCommand();
            commandReader.SendResponse("Indexing " + Path.GetFileName(path) + "...");
            return path;
        }

        private string GetQuery()
        {
            commandReader.SendResponse("Enter words to search (type 'exit' to quit) : ");
            return commandReader.ReadCommand();
        }
    }
}