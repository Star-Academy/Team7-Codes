using System.Collections.Generic;
using InvertedIndexEngine.Model;
using InvertedIndexEngine.ElasticCumminucation;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private QueryHandler queryHandler;
        private bool initialized = false;

        private IConfiguration _config;

        public SearchService(IConfiguration config)
        {
            _config = config;
        }

        private void Setup()
        {
            var elasticSearchIndexName = _config["ElasticSearchIndexName"];
            var elasticSearchAddress = _config["ElasticSearchAddress"];
            ElasticClientFactory.CreateInitialClient(elasticSearchAddress);
            queryHandler = new QueryHandler(elasticSearchIndexName);
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