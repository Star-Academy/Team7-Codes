using Nest;
using System.IO;

namespace InvertedIndexEngine.Model
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
            foreach (var file in files)
            {
                var document = ReadSingleDocument(file, indexName);
                bulkDescriptor.Index<Document>(x => x
                    .Index(indexName)
                    .Document(document));
            }
        }

        private Document ReadSingleDocument(string file, string indexName)
        {
            var document = new Document()
            {
                Name = System.IO.Path.GetFileName(file),
                Content = File.ReadAllText(file)
            };
            return document;
        }

    }
}