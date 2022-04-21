namespace Catalog.Dtos
{
    // A DTO is used so that we can expose a fixed interface to clients.
   public record ItemDto
   {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
