using Nest;
using System.IO;
using System.Linq;
using ElasticFinderConsoleApp.Model;

namespace ElasticFinderConsoleApp
{
    public class FileReader
    {
        public string Path { get; set; }

        public FileReader(string path)
        {
            Path = path;
        }

        public void ReadDirectory(BulkDescriptor bulkDescriptor, string indexName)
        {
            var files = Directory.GetFiles(Path);
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
            document.Name = System.IO.Path.GetFileName(file);
            document.Content = File.ReadAllText(file);
            return document;
        }

    }
}