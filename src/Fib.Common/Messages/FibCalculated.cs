using System;
using Fib.Common.Bus.Abstractions;

namespace Fib.Common.Messages
{
    public class FibCalculated : IEvent
    {
        public int For { get;  }
        public int Value { get; }
        public Guid Id { get; }
        public DateTime CreatedAt { get; }

        public FibCalculated(int @for, int value)
        {
            Value = value;
            For = @for;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"{nameof(For)}: {For}, {nameof(Value)}: {Value}, {nameof(Id)}: {Id}, {nameof(CreatedAt)}: {CreatedAt}";
        }
    }
}