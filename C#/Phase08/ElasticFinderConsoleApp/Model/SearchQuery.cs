using System.Collections.Generic;

namespace ElasticFinderConsoleApp.Model
{
    public class SearchQuery
    {

        public HashSet<string> MustIncludeWords { get; set; }
        public HashSet<string> IncludeWords { get; set; }
        public HashSet<string> ExcludeWords { get; set; }

        public SearchQuery()
        {
            Initialize();
        }

        public SearchQuery(string query)
        {
            Initialize();
            this.ParseString(query);
        }

        private void Initialize()
        {
            MustIncludeWords = new HashSet<string>();
            IncludeWords = new HashSet<string>();
            ExcludeWords = new HashSet<string>();
        }

        public void ParseString(string query)
        {
            var words = query.Trim().Split(' ');
            if (words[0].Length > 0)
            {
                foreach (var word in words)
                {
                    AddToCorrectSet(word);
                }
            }
        }

        private void AddToCorrectSet(string word)
        {
            switch (word.Substring(0, 1))
            {
                case "+":
                    IncludeWords.Add(word);
                    break;
                case "-":
                    ExcludeWords.Add(word);
                    break;
                default:
                    MustIncludeWords.Add(word);
                    break;
            }
        }
    }
}