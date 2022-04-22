using System.ComponentModel.DataAnnotations;

namespace Dtos
{
    // A DTO is needed so that we can expose a fixed interface to clients.
    public record UpdateItemDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(1,1000)]
        public decimal Price { get; init; }
    }
}
