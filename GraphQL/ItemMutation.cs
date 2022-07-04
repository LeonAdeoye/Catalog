using Catalog.Entities;
using Catalog.Repositories;

namespace Catalog.GraphQL
{
    public record ItemInput(string Name, decimal Price);

    public record ItemPayload(string Name, decimal Price);

    public class ItemMutation
    {
        public async Task<ItemPayload> AddItemAsync(ItemPayload itemPayload, [Service] IItemsRepository itemsRepository)
        {
            Item newItem = new() 
            {
                Id =  Guid.NewGuid(),
                Name = itemPayload.Name, 
                Price = itemPayload.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateItemAsync(newItem);

            return itemPayload;
        }

    }
}
