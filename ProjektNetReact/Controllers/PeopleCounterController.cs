using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ProjektNetReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleCounterController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public PeopleCounterController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public ActionResult<int> GetPeopleCount()
        {
            if (!_cache.TryGetValue("PeopleCount", out int count))
            {
                count = 0; 
            }

            return Ok(count);
        }

        [HttpPost("increment")]
        public ActionResult<int> IncrementCount()
        {
            if (!_cache.TryGetValue("PeopleCount", out int count))
            {
                count = 0; 
            }

            count++;
            _cache.Set("PeopleCount", count);

            return Ok(count);
        }

        [HttpPost("decrement")]
        public ActionResult<int> DecrementCount()
        {
            if (!_cache.TryGetValue("PeopleCount", out int count))
            {
                count = 0; 
            }

            if (count > 0)
            {
                count--;
                _cache.Set("PeopleCount", count);
            }

            return Ok(count);
        }
    }
}
