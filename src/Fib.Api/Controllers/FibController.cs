using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Fib.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FibController : ControllerBase
    {
        private readonly IDistributedCache _db;

        public FibController(IDistributedCache db)
        {
            _db = db;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            var result = await _db.GetAsync($"Fib:{number}");
            if (result != null)
            {
                int? res = JsonConvert.DeserializeObject<int?>(Encoding.UTF8.GetString(result));
                return Ok(res);
            }

            return Content("Result not ready");
        }
    }
}