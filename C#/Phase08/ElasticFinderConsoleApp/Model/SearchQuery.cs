using System.Collections.Generic;

namespace ElasticFinderConsoleApp.Model
{
    public class SearchQuery
    {

        public HashSet<string> MustIncludeTokens { get; set; }
        public HashSet<string> IncludeTokens { get; set; }
        public HashSet<string> ExcludeTokens { get; set; }

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
            MustIncludeTokens = new HashSet<string>();
            IncludeTokens = new HashSet<string>();
            ExcludeTokens = new HashSet<string>();
        }

        public void ParseString(string query)
        {
            var words = query.Split(' ');
            foreach (var word in words)
            {
                AddToCorrectSet(word);
            }
        }

        private void AddToCorrectSet(string word)
        {
            switch (word.Substring(0, 1))
            {
                case "+":
                    IncludeTokens.Add(word);
                    break;
                case "-":
                    ExcludeTokens.Add(word);
                    break;
                default:
                    MustIncludeTokens.Add(word);
                    break;
            }
        }
    }
}