using System;
using Fib.Common.Bus.Abstractions;
using Newtonsoft.Json;

namespace Fib.Common.Messages
{
    public class GetFib : ICommand
    {
        public int Number { get; }

        [JsonConstructor]
        public GetFib(int number)
        {
            Number = number;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreatedAt { get; }

        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, {nameof(Id)}: {Id}, {nameof(CreatedAt)}: {CreatedAt}";
        }
    }
}