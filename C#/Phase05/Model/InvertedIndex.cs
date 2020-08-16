using System.Collections.Generic;
using Phase05.Model.Interface;
using Phase05.CustomException;
using System.Linq;

namespace Phase05.Model
{
    public class InvertedIndex<T, E> : IIndex<T, E>
    {
        public Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> Index { get; }

        public InvertedIndex(Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> index)
        {
            this.Index = index;
        }

        public void Add(IAddQuery<T, E> addQuery)
        {
            if (!Index.TryGetValue(addQuery.Token, out var tokensInfo))
            {
                tokensInfo = new HashSet<ITokenInfo<E>>();
                Index[addQuery.Token] = tokensInfo;
            }

            tokensInfo.Add(addQuery.TokenInfo);
        }

        public List<ITokenInfo<E>> Find(ISearchQuery<T> searchQuery)
        {
            var result = new HashSet<ITokenInfo<E>>();
            ProcessForMustTokens(result, searchQuery.MustIncludeTokens);
            ProcessForIncludeTokens(result, searchQuery.IncludeTokens);
            ProcessForExcludeTokens(result, searchQuery.ExcludeTokens);
            if (result.Count == 0)
            {
                throw new NoResultFoundException();
            }
            return new List<ITokenInfo<E>>(result);
        }

        private void ProcessForMustTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            if (tokens.Count == 0)
            {
                return;
            }

            result.UnionWith(FindSingleToken(tokens.First()));

            foreach (var token in tokens)
            {
                result.IntersectWith(FindSingleToken(token));
            }
        }

        private void ProcessForIncludeTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            foreach (var token in tokens)
            {
                result.UnionWith(FindSingleToken(token));
            }
        }

        private void ProcessForExcludeTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            foreach (var token in tokens)
            {
                result.ExceptWith(FindSingleToken(token));
            }
        }

        private HashSet<ITokenInfo<E>> FindSingleToken(IToken<T> token)
        {
            if (Index.ContainsKey(token))
            {
                return Index[token];
            }
            return new HashSet<ITokenInfo<E>>();
        }
    }
}