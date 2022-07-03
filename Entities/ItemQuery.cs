using Catalog.Repositories;

namespace Catalog.Entities
{
    public class ItemQuery
    {
        private readonly IItemsRepository itemsRepository;
        public ItemQuery(IItemsRepository itemsRepository) 
        {
            this.itemsRepository = itemsRepository;
        }

        public List<Item> Items => itemsRepository.GetItems().ToList();
    }
}
