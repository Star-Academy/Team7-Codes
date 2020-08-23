using System.Collections.Generic;
using Nest;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<HashSet<Document>> Find(SearchQuery query)
        {
            var result = new HashSet<Document>();
            await ProcessMustInclude(result, query.MustIncludeTokens);
            await ProcessInclude(result, query.IncludeTokens);
            await ProcessExclude(result, query.ExcludeTokens);
            return result;
        }

        private async Task ProcessMustInclude(ISet<Document> result, ISet<string> words)
        {
            if (words.Count == 0)
            {
                return;
            }

            result.UnionWith(await FindSingleToken(words.First()));

            foreach (var word in words)
            {
                result.IntersectWith(await FindSingleToken(word));
            }
        }

        private async Task ProcessInclude(ISet<Document> result, ISet<string> words)
        {
            foreach (var word in words)
            {
                result.UnionWith(await FindSingleToken(word));
            }
        }

        private async Task ProcessExclude(ISet<Document> result, ISet<string> words)
        {
            foreach (var word in words)
            {
                result.ExceptWith(await FindSingleToken(word));
            }
        }

        private async Task<HashSet<Document>> FindSingleToken(string word)
        {
            var response = client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Match(m => m
                        .Field(d => d.Content)
                        .Query(word)
            )));
            await ElasticResponseValidator<ISearchResponse<Document>>.ValidateResponse(response);
            return (await response).Documents.ToHashSet<Document>();
        }
    }
}