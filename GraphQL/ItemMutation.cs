using Catalog.Entities;
using Catalog.Repositories;
using HotChocolate.Subscriptions;

namespace Catalog.GraphQL
{
    public record ItemInput(string Name, decimal Price);

    public record ItemPayload(string Name, decimal Price);

    public class ItemMutation
    {
        /*
            mutation
            {
                addItem(itemPayload: { name: "Amazing Mansion" price: 4600})
                { name }
            }
         */
        public async Task<ItemPayload> AddItemAsync(ItemPayload itemPayload, [Service] IItemsRepository itemsRepository, 
            [Service] ITopicEventSender eventSender, CancellationToken cancellationToken)
        {
            Item newItem = new() 
            {
                Id =  Guid.NewGuid(),
                Name = itemPayload.Name, 
                Price = itemPayload.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateItemAsync(newItem);

            await eventSender.SendAsync(nameof(ItemSubscription.OnItemAdded), newItem, cancellationToken);

            return itemPayload;
        }

        public async Task DeleteItemAsync(Guid id, [Service] IItemsRepository itemsRepository)
        {
            await itemsRepository.DeleteItemAsync(id);
        }

    }
}
