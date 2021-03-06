using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchApi.Services;

namespace SearchApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> SearchAsync(string query)
        {
            var result = await searchService.Search(query);
            if (result.Count == 0)
                return NotFound();
            return Ok(result);
        }
    }
}