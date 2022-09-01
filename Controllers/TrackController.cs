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
    [Route("tracks")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackRepository repository;

        public TrackController(ITrackRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TrackDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<TrackDto>> CreateItemAsync(CreateTrackDto itemDto)
        {
            var existingItems = await repository.GetItemsAsync();

            var existingItem = existingItems.Select(i => i).Where(i => i.Name == itemDto.Name).FirstOrDefault();

            if (existingItem != null)
            {
                return Conflict("Duplicate object");
            }

            Track item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Length = itemDto.Length,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateTrackDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            Track updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Length = itemDto.Length
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