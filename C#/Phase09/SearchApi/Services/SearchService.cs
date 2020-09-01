using System.Collections.Generic;
using InvertedIndexEngine.Model;
using InvertedIndexEngine.ElasticCumminucation;
using System.Threading.Tasks;

namespace SearchApi.Services
{
    public class SearchService : ISearchService
    {
        private string indexName = "inverted-index-engine";
        private QueryHandler queryHandler;
        private bool flag = true;
        public void Setup()
        {
            ElasticClientFactory.CreateInitialClient("http://localhost:9200");
            queryHandler = new QueryHandler(indexName);
            flag = false;
        }

        public async Task<HashSet<Document>> Search(string query)
        {
            if(flag)
                Setup();
            var result = await queryHandler.Find(new SearchQuery(query));
            return result;
        }

        


    }
}