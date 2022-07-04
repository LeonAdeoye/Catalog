using Catalog.Entities;
using Catalog.Repositories;
using HotChocolate.Types;

namespace Catalog.GraphQL
{
    public class ItemType : ObjectType<Item>
    {
        protected override void Configure(IObjectTypeDescriptor<Item> descriptor)
        {
            descriptor.Description("Represents an item that exists in the Mincraft world.");

            descriptor.Field(p => p.Id).Ignore();

            descriptor.Field(p => p.Id).ResolveWith<ItemResolvers>(p => p.GetPrices(default!, default!)).Use<IItemsRepository>().Description("List of prices.");
        }

        private class ItemResolvers
        {
            public IEnumerable<Item> GetPrices(Item item, [Service] IItemsRepository itemRepository) => itemRepository.GetItems().Where(p => p.Id == item.Id);
        }
    }
}
