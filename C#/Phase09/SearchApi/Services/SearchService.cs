using System.Collections.Generic;
using InvertedIndexEngine.Model;
using InvertedIndexEngine.ElasticCumminucation;
using System.Threading.Tasks;

namespace SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private QueryHandler queryHandler;
        private bool initialized = false;

        private void Setup()
        {
            ElasticClientFactory.CreateInitialClient("http://localhost:9200");
            var indexName = "inverted-index-engine";
            queryHandler = new QueryHandler(indexName);
            initialized = true;
        }

        public async Task<HashSet<Document>> Search(string query)
        {
            if (!initialized)
            {
                Setup();
            }
            var result = await queryHandler.Find(new SearchQuery(query));
            return result;
        }
    }
}