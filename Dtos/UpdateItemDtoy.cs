using System.ComponentModel.DataAnnotations;

namespace WSEIbackendREST.Dtos
{
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 10000000)]
        public decimal Price { get; init; }
    }

    public record UpdateTrackDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(2500, 10000)]
        public decimal Length { get; init; }
    }

    public record UpdateEmployeeDto
    {
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        [Range(10000000000, 99999999999)]
        public long Pesel { get; init; }
    }
}