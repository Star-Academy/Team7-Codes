using System.Collections.Generic;
using InvertedIndexEngine.Model;
using System.Threading.Tasks;

namespace SearchApi.Services
{
    public interface ISearchService
    {
        void Setup();
        Task<HashSet<Document>> Search(string query);
    }
}