using System.Collections.Generic;


namespace Phase05.Model.Interface
{
    public interface ISearchQuery<T>
    {
        HashSet<IToken<T>> MustIncludeTokens{get; set;}
        HashSet<IToken<T>> IncludeTokens{get; set;}
        HashSet<IToken<T>> ExcludeTokens{get; set;}

        void ParseString(string query);
    }
}