using System.Collections.Generic;
using Phase05.Model.Interface;
using System;
using System.Linq;

namespace Phase05.Model
{
    public class InvertedIndex<T, E> : IIndex<T, E>
    {
        public Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> Index { get; }

        public InvertedIndex(Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> Index)
        {
            this.Index = Index;
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
            return new List<ITokenInfo<E>>(result);
        }

        private void ProcessForMustTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            if (tokens.Count == 0)
            {
                return;
            }

            try
            {
                result.UnionWith(FindSingleToken(tokens.First()));
            }
            catch
            {

            }

            foreach (var token in tokens)
            {
                try
                {
                    result.IntersectWith(FindSingleToken(token));
                }
                catch
                {

                }
            }


        }

        private void ProcessForIncludeTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            foreach (var token in tokens)
            {
                try
                {
                    result.UnionWith(FindSingleToken(token));
                }
                catch
                {

                }
            }
        }

        private void ProcessForExcludeTokens(ISet<ITokenInfo<E>> result, ISet<IToken<T>> tokens)
        {
            foreach (var token in tokens)
            {
                try
                {
                    result.ExceptWith(FindSingleToken(token));
                }
                catch
                {

                }
            }
        }

        private HashSet<ITokenInfo<E>> FindSingleToken(IToken<T> token)
        {
            if (Index.ContainsKey(token))
            {
                return Index[token];
            }

            throw new InvalidOperationException("token not exist");

        }
    }
}