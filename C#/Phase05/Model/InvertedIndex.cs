using System.Collections.Generic;
using Phase05.Model.Interface;

namespace Phase05.Model
{
    public class InvertedIndex : IIndex
    {
        public void Add(IAddQuery addQuery)
        {
            throw new System.NotImplementedException();
        }

        public List<ISearchQuery> Find(ISearchQuery searchQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}