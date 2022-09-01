using System;

namespace WSEIbackendREST.Entities
{
    public record Item
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }

    public record Track
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Length { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }

    public record Employee
    {
        public Guid Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public long Pesel { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}