using System.ComponentModel.DataAnnotations;

namespace Catalog.Entities
{
    // Used Querytype instead of below annotation.
    // [GraphQLDescription("Represents an item that exists in the Mincraft world.")]
    public record Item
    {
        [Key]
        // Used We are not going to expose the Id field in GraphQL
        // [GraphQLDescription("The unqiue Id of the item that exists in the Mincraft world.")] 
        public Guid Id { get; init; }
        [Required]
        [GraphQLDescription("The name of item that exists in the Minecraft world.")]
        public string Name { get; init; }
        [Required]
        [GraphQLDescription("The price of item that exists in the Minecraft world.")]
        public decimal Price { get; init; }
        [GraphQLDescription("The creation date of the item that exists in the Minecraft world.")]
        public DateTimeOffset CreatedDate { get; init; }
    }
}
