using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.controllers
{
    [ApiController]
    [Route("/items")] // You can use "[Controller]" to default to controller prefix: /Items or hardcode: /items.
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository) => this.repository = repository;
      
        // GEt /items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.asDto());
            return items;
        }

        // Get /items{id}
        [HttpGet("{id}")]
        // You need to return ActionResult because you can either return an item or Http Code.
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if(item is null)
            {
                return NotFound();
            }
            return item.asDto();
        }
    }
}
