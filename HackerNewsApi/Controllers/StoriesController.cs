using HackerNewsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _service;
        public StoriesController(IStoryService service)
        {
            _service = service;
        }

        [HttpGet("best")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            //validate input
            if(n <= 0)
            {
                return BadRequest("n must be than than 0");
            }
            var result = await _service.GetBestStoriesAsync(n);

            return Ok(result);
        }

    }
}
