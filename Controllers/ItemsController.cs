using System.Collections.Generic;
using WSEIbackendREST.Entities;
using WSEIbackendREST.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WSEIbackendREST.Dtos;
using System.Threading.Tasks;

namespace WSEIbackendREST.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            var existingItems = await repository.GetItemsAsync();

            var existingItem = existingItems.Select(i => i).Where(i => i.Name == itemDto.Name).FirstOrDefault();

            if (existingItem != null)
            {
                return Conflict("Duplicate object");
            }

            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }

        [HttpDelete("all")]
        public async Task<ActionResult> DeleteAllItemsAsync()
        {
            var existingItems = await repository.GetItemsAsync();

            foreach (var item in existingItems)
            {
                await repository.DeleteItemAsync(item.Id);
            }

            return NoContent();
        }
    }
}