using Catalog.Entities;
using Catalog.Repositories;

namespace Catalog.GraphQL
{
    public class ItemQuery
    {
        public List<string> Names([Service] IItemsRepository repository) => repository.GetItems().Select(x => x.Name).ToList();

        public Task<IEnumerable<Item>> Items([Service] IItemsRepository repository) => repository.GetItemsAsync();
    }
}
