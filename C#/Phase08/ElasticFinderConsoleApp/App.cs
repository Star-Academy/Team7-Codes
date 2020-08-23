using System.IO;

namespace ElasticFinderConsoleApp
{
    public class App
    {
        private readonly ICommandReader commandReader;

        public App(ICommandReader commandReader)
        {
            this.commandReader = commandReader;
        }

        public void Run()
        {
            //var path = GetPath();
            
            var indexName = "my-index1";

            //var documentIndexer = new DocumentIndexer(indexName);
            //documentIndexer.IndexDocuments(new FileReader(path));
            
            CommandHandler(new QueryHandler(indexName));
        }

        private void CommandHandler(QueryHandler queryHandler)
        {
            string command;
            while (!(command = GetQuery()).Equals("exit"))
            {
                var searchQuery = new SearchQuery(command);
                var result = queryHandler.Find(searchQuery);
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