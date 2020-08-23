using Nest;
using System.IO;
using System.Linq;

namespace ElasticFinderConsoleApp
{
    public class FileReader
    {
        public string path { get; set; }
        public void ReadDirectory(BulkDescriptor bulkDescriptor, string indexName)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files.Where(x => File.Exists(x)))
            {
                var document = ReadSingleDocument(file, bulkDescriptor, indexName);
                bulkDescriptor.Index<Document>(x => x
                    .Index(indexName)
                    .Document(document));
            }
        }

        private Document ReadSingleDocument(string file, BulkDescriptor bulkDescriptor, string indexName)
        {
            var document = new Document();
            document.Name = Path.GetFileName(file);
            document.Content = File.ReadAllText(file);
            return document;
        }

    }
}