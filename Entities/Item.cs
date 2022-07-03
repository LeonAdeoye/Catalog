using System.ComponentModel.DataAnnotations;

namespace Catalog.Entities
{
    public record Item
    {
        [Key]
        public Guid Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
