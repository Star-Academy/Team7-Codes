using System.Collections.Generic;

namespace Phase05.Model.Interface
{
    public interface IIndex
    {
         void Add(IAddQuery addQuery);

         List<ISearchQuery> Find(ISearchQuery searchQuery);
    }
}