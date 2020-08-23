using System.Collections.Generic;
using Nest;
using System.Linq;
using System;

namespace ElasticFinderConsoleApp
{
    public class QueryHandler
    {
        private readonly IElasticClient client;
        private readonly string indexName;

        public QueryHandler(string indexName)
        {
            this.client = ElasticClientFactory.client;
            this.indexName = indexName;
        }

        public HashSet<Document> Find(SearchQuery query)
        {
            var result = new HashSet<Document>();
            ProcessMustInclude(result, query.MustIncludeTokens);
            ProcessInclude(result, query.IncludeTokens);
            ProcessExclude(result, query.ExcludeTokens);
            return result;
        }

        private void ProcessMustInclude(ISet<Document> result, ISet<string> words)
        {
            if (words.Count == 0)
            {
                return;
            }

            result.UnionWith(FindSingleToken(words.First()));

            foreach (var word in words)
            {
                result.IntersectWith(FindSingleToken(word));
            }
        }

        private void ProcessInclude(ISet<Document> result, ISet<string> words)
        {
            foreach (var word in words)
            {
                result.UnionWith(FindSingleToken(word));
            }
        }

        private void ProcessExclude(ISet<Document> result, ISet<string> words)
        {
            foreach (var word in words)
            {
                result.ExceptWith(FindSingleToken(word));
            }
        }

        private HashSet<Document> FindSingleToken(string word)
        {
            var response = client.Search<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(d => d.Content)
                        .Query(word)
            )));
            Console.WriteLine(response.Documents.Count);       
            var tmp = response.Documents.ToHashSet<Document>();
            Console.WriteLine(tmp.First().Name);
            return tmp;
        }
    }
}