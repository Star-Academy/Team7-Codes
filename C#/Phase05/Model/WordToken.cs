using System.Collections.Generic;
using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class WordToken : IToken<string>
    {
        
        public string Content { get; set; }

        public WordToken(string word)
        {
            this.Content = word;
        }

        public override bool Equals(object obj)
        {
            var word = obj as WordToken;
            
            if(word == null)
                return false;
            else
                return this.Content.Equals(word.Content);
        }

        public override int GetHashCode()
        {
            return 1997410482 + EqualityComparer<string>.Default.GetHashCode(Content);
        }
    }
}