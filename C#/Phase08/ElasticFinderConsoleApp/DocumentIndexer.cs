using Nest;
using System.Threading.Tasks;

namespace ElasticFinderConsoleApp
{
    public class DocumentIndexer
    {
        public string IndexName {get; set;}
        private IElasticClient Client = ElasticClientFactory.client;

        public DocumentIndexer(string indexName)
        {
            IndexName = indexName;
        }
        public async Task CreateIndex()
        {
            var responseTask = Client.Indices.CreateAsync(IndexName, s => s
            .Map<Document>(d => d
                .Properties(pr => pr
                    .Text(t => t
                        .Name(n => n.Content)
                        .Analyzer("standard"))
                    .Text(t => t
                        .Name(n => n.Name)))));

            await ElasticResponseValidator<CreateIndexResponse>.ValidateResponseAndLogConsole(responseTask);
        }

        public async Task DeleteIndex()
        {
            var responseExist = Client.Indices.ExistsAsync(IndexName);
            await ElasticResponseValidator<ExistsResponse>.ValidateResponseAndLogConsole(responseExist);
            if(responseExist.Result.Exists)
            {
                var responseTask = Client.Indices.DeleteAsync(IndexName);
                await ElasticResponseValidator<DeleteIndexResponse>.ValidateResponseAndLogConsole(responseTask);
            }
        }

        public async Task IndexDocuments(FileReader fileReader)
        {
            var bulkDescriptor = AddToBulkDescriptor(fileReader);
            var responseTask = Client.BulkAsync(bulkDescriptor);
            await ElasticResponseValidator<BulkResponse>.ValidateResponseAndLogConsole(responseTask);
        }

        private BulkDescriptor AddToBulkDescriptor(FileReader fileReader)
        {
            var bulkDescriptor = new BulkDescriptor();
            fileReader.ReadDirectory(bulkDescriptor, IndexName);
            return bulkDescriptor;
        }
    }
}