using System;

namespace Fib.Common.Bus.Abstractions
{
    public interface ICommand
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
    }
}