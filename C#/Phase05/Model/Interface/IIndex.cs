using System.Collections.Generic;

namespace Phase05.Model.Interface
{
    public interface IIndex<T, E>
    {
        void Add(IAddQuery<T, E> addQuery);

        List<ITokenInfo<E>> Find(ISearchQuery<T> searchQuery);
    }
}