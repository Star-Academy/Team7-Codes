using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class DocumentInfo<E> : ITokenInfo<E>
    {
        public E Content{get; set;}
    }
}