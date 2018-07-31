using System.Threading.Tasks;

namespace Fib.Api
{
    public interface IStorage
    {
        Task<int?> Get(int number);
        Task Store(int num, int value);
    }
}