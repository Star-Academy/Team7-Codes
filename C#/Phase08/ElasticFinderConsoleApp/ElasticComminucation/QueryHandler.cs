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
            var boolQuery = CreateBoolQuery(query);
            var searchResponse = client.SearchAsync<Document>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => boolQuery)));
            await ElasticResponseValidator<ISearchResponse<Document>>.ValidateResponseAndLogConsole(searchResponse);
            return (await searchResponse).Documents.ToHashSet<Document>();
        }

        private BoolQuery CreateBoolQuery(SearchQuery query)
        {
            var mustIncludeWordsQueryContainerList = CreateWordsQueryContainerList(query.MustIncludeWords);
            var includeWordsQueryContainerList = CreateWordsQueryContainerList(query.IncludeWords);
            var excludeWordsQueryContainerList = CreateWordsQueryContainerList(query.ExcludeWords);

            var boolQuery = new BoolQuery
            {
                Must = mustIncludeWordsQueryContainerList,
                Should = includeWordsQueryContainerList,
                MustNot = excludeWordsQueryContainerList
            };

            return boolQuery;
        }

        private List<QueryContainer> CreateWordsQueryContainerList(HashSet<string> words)
        {
            var queryContainerList = new List<QueryContainer>();
            foreach (var word in words)
            {
                var matchQuery = new MatchQuery();
                matchQuery.Field = "content";
                matchQuery.Query = word;
                queryContainerList.Add(matchQuery);
            }
            return queryContainerList;
        }

    }
}