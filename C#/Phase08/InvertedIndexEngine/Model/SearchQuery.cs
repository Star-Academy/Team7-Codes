using System.Collections.Generic;

namespace InvertedIndexEngine.Model
{
    public class SearchQuery
    {

        public readonly HashSet<string> MustIncludeWords;
        public readonly HashSet<string> IncludeWords;
        public readonly HashSet<string> ExcludeWords;

        public SearchQuery(string query)
        {
            MustIncludeWords = new HashSet<string>();
            IncludeWords = new HashSet<string>();
            ExcludeWords = new HashSet<string>();
            this.ParseString(query);
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