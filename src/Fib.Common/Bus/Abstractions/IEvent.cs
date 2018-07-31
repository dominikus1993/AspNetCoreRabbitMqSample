using System;

namespace Fib.Common.Bus.Abstractions
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
    }
}