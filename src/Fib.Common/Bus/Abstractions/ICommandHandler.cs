using System.Threading.Tasks;

namespace Fib.Common.Bus.Abstractions
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}