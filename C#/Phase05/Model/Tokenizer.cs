using Phase05.Model.Interface;
using System.IO;

namespace Phase05.Model
{
    public class Tokenizer
    {
        private IIndex<string, string> index;
        private Normalizer normalizer;

        public Tokenizer(IIndex<string, string> index, Normalizer normalizer)
        {
            this.index = index;
            this.normalizer = normalizer;
        }

        public void TokenizeDirectory(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    ProcessFile(file);
                }
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
            var normalizedWord = normalizer.Normalize(new WordToken(word));
            AddQuery<string, string> addQuery;
            addQuery = new AddQuery<string, string>(normalizedWord
                ,new DocumentInfo(documentName));
            index.Add(addQuery);
        }
    }
}