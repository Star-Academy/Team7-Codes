using Phase05.Model.Interface;
using System.Collections.Generic;
using System;

namespace Phase05.Model
{
    public class SearchQuery<T> : ISearchQuery<T>
    {

        public HashSet<IToken<T>> MustIncludeTokens { get; }
        public HashSet<IToken<T>> IncludeTokens { get; }
        public HashSet<IToken<T>> ExcludeTokens { get; }

        public SearchQuery()
        {
            MustIncludeTokens = new HashSet<IToken<T>>();
            IncludeTokens = new HashSet<IToken<T>>();
            ExcludeTokens = new HashSet<IToken<T>>();
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
            var wordToken = new WordToken(word) as IToken<T>;
            switch (word.Substring(0, 1))
            {
                case "+":
                    IncludeTokens.Add(wordToken);
                    break;
                case "-":
                    ExcludeTokens.Add(wordToken);
                    break;
                default:
                    MustIncludeTokens.Add(wordToken);
                    break;
            }
        }
    }
}