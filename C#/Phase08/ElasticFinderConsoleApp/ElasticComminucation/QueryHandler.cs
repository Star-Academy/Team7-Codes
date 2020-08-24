using System.Collections.Generic;
using Nest;
using System.Linq;
using System.Threading.Tasks;
using ElasticFinderConsoleApp.Model;

namespace ElasticFinderConsoleApp.ElasticCumminucation
{
    public class QueryHandler
    {
        private readonly IElasticClient client;
        private readonly string indexName;

        public QueryHandler(string indexName)
        {
            this.client = ElasticClientFactory.GetElasticClient();
            this.indexName = indexName;
        }

        public async Task<HashSet<Document>> Find(SearchQuery query)
        {
            var result = new HashSet<Document>();
            // await ProcessMustInclude(result, query.MustIncludeTokens);
            // await ProcessInclude(result, query.IncludeTokens);
            // await ProcessExclude(result, query.ExcludeTokens);
            result = await SearchElastic(query);
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
            await ElasticResponseValidator<ISearchResponse<Document>>.ValidateResponseAndLogConsole(response);
            return (await response).Documents.ToHashSet<Document>();
        }

        private async Task<HashSet<Document>> SearchElastic(SearchQuery searchQuery)
        {
            var mustIncludeQueryContainerList = new List<QueryContainer>();
            foreach (var word in searchQuery.MustIncludeTokens)
            {
                var matchQuery = new MatchQuery();
                matchQuery.Field = "content";
                matchQuery.Query = word;
                mustIncludeQueryContainerList.Add(matchQuery);
            }
            var query = new BoolQuery
            {
                Must = mustIncludeQueryContainerList
            };

            var response = client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => query)
            ));
            await ElasticResponseValidator<ISearchResponse<Document>>.ValidateResponseAndLogConsole(response);
            return (await response).Documents.ToHashSet<Document>();
        }
    }
}