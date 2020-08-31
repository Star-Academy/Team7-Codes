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
        public async Task Setup()
        {
            var path = "/media/hassan/new part/code-star/EnglishData";
            ElasticClientFactory.CreateInitialClient("http://localhost:9200");
            var documentIndexer = new DocumentIndexer(indexName);
            await documentIndexer.DeleteIndex();
            await documentIndexer.CreateIndex();
            await documentIndexer.IndexDocuments(new FileReader(path));
            queryHandler = new QueryHandler(indexName);
        }

        public async Task<HashSet<Document>> Search(string query)
        {
            await Setup();
            var result = await queryHandler.Find(new SearchQuery(query));
            System.Console.WriteLine(result.Count);
            return result;
        }

        


    }
}