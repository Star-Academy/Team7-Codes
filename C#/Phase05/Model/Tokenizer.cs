using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class Tokenizer
    {
        private IIndex<string, string> index;

        public Tokenizer(IIndex<string, string> index)
        {
            this.index = index;
        }

        public void TokenizeDirectory(string path)
        {
            
        }
    }
}