using System.Collections.Generic;
using InvertedIndexEngine.Model;
using System.Threading.Tasks;

namespace SearchApi.Services
{
    public interface ISearchService
    {
        Task<HashSet<Document>> Search(string query);
    }
}