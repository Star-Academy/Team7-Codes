using System.Collections.Generic;
using Nest;
using System.Linq;
using System.Threading.Tasks;
using InvertedIndexEngine.Model;

namespace InvertedIndexEngine.ElasticCumminucation
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
                    
            await ElasticResponseValidator.ValidateResponseAndLogConsole(searchResponse);
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
            var queryContainerList = words.Select(word => (QueryContainer)new MatchQuery
                {
                    Field = "content",
                    Query = word
                }).ToList();
            
            return queryContainerList;
        }

    }
}