using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class Tokenizer<T, E>
    {
        private IIndex<T, E> index;

        public Tokenizer(IIndex<T, E> index)
        {
            this.index = index;
        }

        public void TokenizeDirectory(string path)
        {
            
        }
    }
}