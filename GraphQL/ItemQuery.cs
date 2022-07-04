using Catalog.Entities;
using Catalog.Repositories;

namespace Catalog.GraphQL
{
    public class ItemQuery
    {
        public List<string> Names([Service] IItemsRepository repository) => repository.GetItems().Select(x => x.Name).ToList();

        [UseFiltering]
        [UseSorting]
        /*
         *  query
            {
              items(where: {price: {lte:100}})
              {
                name
                price
                createdDate
              }
            }
            query
            {
              items(order: {price: ASC})
              {
                name
                price
                createdDate
              }
            }
        */
        public Task<IEnumerable<Item>> Items([Service] IItemsRepository repository) => repository.GetItemsAsync();
    }
}
