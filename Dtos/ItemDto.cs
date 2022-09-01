using System;

namespace WSEIbackendREST.Dtos
{
    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }

    public record TrackDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Length { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }

    public record EmployeeDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public decimal Pesel { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}