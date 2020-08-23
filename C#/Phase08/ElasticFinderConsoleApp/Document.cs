using System;

namespace ElasticFinderConsoleApp
{
    public class Document
    {
        public string Name{get; set;}
        public string Content{get; set;}

        public override bool Equals(object obj)
        {
            return obj is Document document &&
                   Name == document.Name &&
                   Content == document.Content;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Content);
        }
    }
}