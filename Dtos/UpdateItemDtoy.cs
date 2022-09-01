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
}