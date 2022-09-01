using System;
using System.Collections.Generic;
using WSEIbackendREST.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace WSEIbackendREST.Repositories
{
    /// <summary>
    /// Legacy class for working with in memory repositories (currently running on MongoDB)
    /// </summary>
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new List<Item>()
        {
            new Item { Id = Guid.NewGuid(), Name = "Combustion Engine", Price = 250000, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Gearbox", Price = 150000, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Electric Engine", Price = 120000, CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}