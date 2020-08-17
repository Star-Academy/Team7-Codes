using Phase05.Model;
using Phase05.Model.Interface;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFinderApp
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
            var path = GetPath();
            var invertedIndex = new InvertedIndex<string, string>
                (new Dictionary<IToken<string>, HashSet<ITokenInfo<string>>>());
            new Tokenizer(invertedIndex).TokenizeDirectory(path);
            CommandHandler(invertedIndex);
        }

        private void CommandHandler(IIndex<string, string> invertedIndex)
        {
            string command;
            while (!(command = GetQuery()).Equals("exit"))
            {
                var searchQuery = new SearchQuery<string>(command);
                var result = invertedIndex.Find(searchQuery);
                commandReader.SendResponse(result.Count + " results found :");
                result.ForEach(x => commandReader.SendResponse("\t" + x.Content));
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