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
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetItemAsync(Guid id)
        {
            var item = await repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateItemAsync(CreateEmployeeDto itemDto)
        {
            Employee item = new()
            {
                Id = Guid.NewGuid(),
                FirstName = itemDto.FirstName,
                LastName = itemDto.LastName,
                Pesel = itemDto.Pesel,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateEmployeeDto itemDto)
        {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            Employee updatedItem = existingItem with
            {
                FirstName = itemDto.FirstName,
                LastName = itemDto.LastName,
                Pesel = itemDto.Pesel
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