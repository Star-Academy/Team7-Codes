using System;
using Nest;

namespace ElasticFinderConsoleApp.ElasticCumminucation
{
    internal static class ElasticClientFactory
    {
        public static IElasticClient Client
        {
            get
            {
                Client ??= InitialClient();
                return Client;
            }
            set
            {
                Client = value;
            }
        }


        private static IElasticClient InitialClient()
        {
            var uri = new Uri("http://localhost:9200");
            var connectionSettings = new ConnectionSettings(uri);
            return new ElasticClient(connectionSettings);
        }


    }
}