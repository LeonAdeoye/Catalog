using Catalog.Entities;

namespace Catalog.GraphQL
{
    public class ItemSubscription
    {
        [Subscribe]
        [Topic]
        public Item OnItemAdded([EventMessage] Item item) => item;
    }
}
