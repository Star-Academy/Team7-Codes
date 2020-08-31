using Nest;
using InvertedIndexEngine.Model;
using System.Threading.Tasks;

namespace InvertedIndexEngine.ElasticCumminucation
{
    public class DocumentIndexer
    {
        private string indexName;
        private IElasticClient client = ElasticClientFactory.GetElasticClient();

        public DocumentIndexer(string indexName)
        {
            this.indexName = indexName;
        }
        public async Task CreateIndex()
        {
            var responseTask = client.Indices.CreateAsync(indexName, s => s
            .Map<Document>(d => d
                .Properties(pr => pr
                    .Text(t => t
                        .Name(n => n.Content)
                        .Analyzer("standard"))
                    .Text(t => t
                        .Name(n => n.Name)))));

            await ElasticResponseValidator.ValidateResponseAndLogConsole(responseTask);
        }

        public async Task DeleteIndex()
        {
            var responseExist = client.Indices.ExistsAsync(indexName);
            await ElasticResponseValidator.ValidateResponseAndLogConsole(responseExist);
            if (responseExist.Result.Exists)
            {
                var responseTask = client.Indices.DeleteAsync(indexName);
                await ElasticResponseValidator.ValidateResponseAndLogConsole(responseTask);
            }
        }

        public async Task IndexDocuments(FileReader fileReader)
        {
            var bulkDescriptor = AddToBulkDescriptor(fileReader);
            var responseTask = client.BulkAsync(bulkDescriptor);
            await ElasticResponseValidator.ValidateResponseAndLogConsole(responseTask);
        }

        private BulkDescriptor AddToBulkDescriptor(FileReader fileReader)
        {
            var bulkDescriptor = new BulkDescriptor();
            fileReader.ReadDirectory(bulkDescriptor, indexName);
            return bulkDescriptor;
        }
    }
}