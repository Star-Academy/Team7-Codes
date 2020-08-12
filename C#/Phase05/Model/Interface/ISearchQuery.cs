using System.Collections.Generic;


namespace Phase05.Model.Interface
{
    public interface ISearchQuery<T>
    {
        HashSet<IToken<T>> MustIncludeTokens{get;}
        HashSet<IToken<T>> IncludeTokens{get;}
        HashSet<IToken<T>> ExcludeTokens{get;}

        void ParseString(string query);
    }
}