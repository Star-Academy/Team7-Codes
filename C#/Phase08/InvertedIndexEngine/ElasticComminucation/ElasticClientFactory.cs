using System;
using Nest;

namespace InvertedIndexEngine.ElasticCumminucation
{
    public static class ElasticClientFactory
    {
        private static IElasticClient client;

        public static void CreateInitialClient(string address)
        {
            var uri = new Uri(address);
            var connectionSettings = new ConnectionSettings(uri);
            client = new ElasticClient(connectionSettings);
        }

        public static IElasticClient GetElasticClient()
        {
            return client;
        }
    }
}

