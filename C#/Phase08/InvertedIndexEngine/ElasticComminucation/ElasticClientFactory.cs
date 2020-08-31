using System;
using Nest;

namespace InvertedIndexEngine.ElasticCumminucation
{
    internal static class ElasticClientFactory
    {
        private static IElasticClient client = CreateInitialClient();

        private static IElasticClient CreateInitialClient()
        {
            var uri = new Uri("http://localhost:9200");
            var connectionSettings = new ConnectionSettings(uri);
            return new ElasticClient(connectionSettings);
        }

        public static IElasticClient GetElasticClient()
        {
            return client;
        }
    }
}

