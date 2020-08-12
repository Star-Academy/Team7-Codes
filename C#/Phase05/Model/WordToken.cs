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
    }
}