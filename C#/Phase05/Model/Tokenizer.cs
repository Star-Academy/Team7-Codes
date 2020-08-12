using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class Tokenizer
    {
        private IIndex index;

        public Tokenizer(IIndex index)
        {
            this.index = index;
        }

        public void TokenizeDirectory(string path)
        {
            
        }
    }
}