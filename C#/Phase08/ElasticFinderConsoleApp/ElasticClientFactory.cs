using System;
using Nest;

namespace ElasticFinderConsoleApp
{
    internal static class ElasticClientFactory 
    {
        public readonly static IElasticClient client = CreateInitialClient();

        private static IElasticClient CreateInitialClient()
        {
            var uri = new Uri ("http://localhost:9200");
            var connectionSettings = new ConnectionSettings (uri);
            connectionSettings.EnableDebugMode();
            return new ElasticClient (connectionSettings);
        }

        
    }
}