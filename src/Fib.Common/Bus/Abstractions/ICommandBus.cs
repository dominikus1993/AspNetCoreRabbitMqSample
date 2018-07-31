using System;
using System.Threading.Tasks;

namespace Fib.Common.Bus.Abstractions
{
    public interface ICommandBus
    {
        Task SendAsync(ICommand command);
    }
}