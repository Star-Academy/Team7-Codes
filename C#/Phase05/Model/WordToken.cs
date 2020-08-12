using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class WordToken : IToken
    {
        public string Word { get; set; }

        public WordToken(string word)
        {
            this.Word = word;
        }
    }
}