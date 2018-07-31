using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Fib.Common.Bus.Abstractions;
using Fib.Common.Messages;
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
        private readonly IBus _bus;
        
        public FibController(IDistributedCache db, IBus bus)
        {
            _db = db;
            _bus = bus;
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
        
        [HttpPost("{number}")]
        public async Task<IActionResult> Post(int number)
        {
            var result = await _db.GetAsync($"Fib:{number}");
            if (result == null)
            {
                await _bus.SendAsync("Test", new GetFib(number));
            }

            return Accepted($"fib/{number}", null);
        }
    }
}