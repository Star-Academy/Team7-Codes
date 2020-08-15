using System.Collections.Generic;
using Phase05.Model.Interface;
using System;

namespace Phase05.Model
{
    public class InvertedIndex<T, E> : IIndex<T, E>
    {
        public Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> Dictionary { get; }

        public InvertedIndex(Dictionary<IToken<T>, HashSet<ITokenInfo<E>>> dictionary)
        {
            this.Dictionary = dictionary;
        }

        public void Add(IAddQuery<T, E> addQuery)
        {
            if (Dictionary.ContainsKey(addQuery.Token))
            {
                Dictionary[addQuery.Token].Add(addQuery.TokenInfo);
            }
            else
            {
                Dictionary.Add(
                    addQuery.Token,
                    new HashSet<ITokenInfo<E>>() {
                        addQuery.TokenInfo
                    });
            }
        }

        public List<ITokenInfo<E>> Find(ISearchQuery<T> searchQuery)
        {
            var result = new HashSet<ITokenInfo<E>>();
            ProcessForMustTokens(result, searchQuery.MustIncludeTokens);
            ProcessForIncludeTokens(result, searchQuery.IncludeTokens);
            ProcessForExcludeTokens(result, searchQuery.ExcludeTokens);
            return new List<ITokenInfo<E>>(result);
        }

        private void ProcessForMustTokens(HashSet<ITokenInfo<E>> result, HashSet<IToken<T>> tokens)
        {   
            if(tokens.Count == 0)
            {
                return;
            }
            foreach (var token in tokens)
            {
                try
                {
                    result.UnionWith(FindSingleToken(token));
                    break;
                }
                catch
                {

                }
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

        private void ProcessForIncludeTokens(HashSet<ITokenInfo<E>> result, HashSet<IToken<T>> tokens)
        {
            if(tokens.Count == 0)
            {
                return;
            }
            foreach(var token in tokens)
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

        private void ProcessForExcludeTokens(HashSet<ITokenInfo<E>> result, HashSet<IToken<T>> tokens)
        {
            if(tokens.Count == 0)
            {
                return;
            }
            foreach(var token in tokens)
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
            if (Dictionary.ContainsKey(token))
            {
                return Dictionary[token];
            }
            else
            {
                throw new InvalidOperationException("token not exist");
            }
        }
    }
}