using Phase05.Model.Interface;
using System.Collections.Generic;

namespace Phase05.Model
{
    public class SearchQuery<T> : ISearchQuery<T>
    {
        public HashSet<IToken<T>> MustIncludeTokens{get;}
        public HashSet<IToken<T>> IncludeTokens{get;}
        public HashSet<IToken<T>> ExcludeTokens{get;}

        public void ParseString(string query)
        {
            throw new System.NotImplementedException();
        }
    }
}