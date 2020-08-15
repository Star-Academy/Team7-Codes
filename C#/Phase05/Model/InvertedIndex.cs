using System.Collections.Generic;
using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class InvertedIndex<T, E> : IIndex<T, E>
    {
        private Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> Dictionary ;

        public InvertedIndex(Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public void Add(IAddQuery<T, E> addQuery)
        {
            throw new System.NotImplementedException();
        }

        public List<ITokenInfo<E>> Find(ISearchQuery<T> searchQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}