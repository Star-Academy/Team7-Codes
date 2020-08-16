using Phase05.Model.Interface;
using System.IO;
using System.Linq;

namespace Phase05.Model
{
    public class Tokenizer
    {
        private readonly IIndex<string, string> index;

        public Tokenizer(IIndex<string, string> index)
        {
            this.index = index;
        }

        public void TokenizeDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files.Where(x => File.Exists(x)))
            {
                    ProcessFile(file);
            }
        }

        private void ProcessFile(string file)
        {
            using (var fileStreamReader = File.OpenText(file))
            {
                string line = "";
                while ((line = fileStreamReader.ReadLine()) != null)
                {
                    ProcessLine(file, line);
                }
            }
        }

        private void ProcessLine(string file, string line)
        {
            var words = line.Split(' ');
            foreach (var word in words)
            {
                ProcessWord(file, word);
            }
        }

        private void ProcessWord(string file, string word)
        {
            var documentName = Path.GetFileName(file);
            var normalizedWord = new Normalizer().Normalize(new WordToken(word));
            AddQuery<string, string> addQuery;
            addQuery = new AddQuery<string, string>(normalizedWord
                , new DocumentInfo(documentName));
            index.Add(addQuery);
        }
    }
}