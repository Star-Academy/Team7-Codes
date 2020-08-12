using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class WordToken<T> : IToken<T>
    {
        public T Content{get; set;}


    }

}