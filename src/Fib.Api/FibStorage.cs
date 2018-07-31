using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Fib.Api
{
    public class FibStorage : IStorage
    {
        public static IDictionary<int, int> _cache = new Dictionary<int, int>();
        private ILogger<FibStorage> _logger;
        private string Key(int number) => $"Fib:{number}";

        public FibStorage(ILogger<FibStorage> logger)
        {
            _logger = logger;
            _logger.LogInformation($"Create Instance {DateTime.UtcNow}");
        }

        public async Task<int?> Get(int number)
        {
            var success = _cache.TryGetValue(number, out number);
            if (success)
            {
                return number;
            }

            return null;
        }

        public async Task Store(int num, int value)
        {
            _cache.Add(num, value);
        }
    }
}