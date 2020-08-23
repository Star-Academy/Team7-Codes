using Nest;

namespace ElasticFinderConsoleApp
{
    public class DocumentIndexer
    {
        public string IndexName {get; set;}
        private IElasticClient Client = ElasticClientFactory.client;

        public DocumentIndexer(string indexName)
        {
            IndexName = indexName;
            CreateIndex();
        }
        public void CreateIndex()
        {
            Client.Indices.Create(IndexName, s => s
            .Map<Document>(d => d
                .Properties(pr => pr
                    .Text(t => t
                        .Name(n => n.Content)
                        .Analyzer("standard"))
                    .Text(t => t
                        .Name(n => n.Name)))));
        }

        public void DeleteIndex()
        {
            Client.Indices.Delete(IndexName);
        }

        public void IndexDocuments(FileReader fileReader)
        {
            var bulkDescriptor = AddToBulkDescriptor(fileReader);
            Client.Bulk(bulkDescriptor);
        }

        private BulkDescriptor AddToBulkDescriptor(FileReader fileReader)
        {
            var bulkDescriptor = new BulkDescriptor();
            fileReader.ReadDirectory(bulkDescriptor, IndexName);
            return bulkDescriptor;
        }
    }
}