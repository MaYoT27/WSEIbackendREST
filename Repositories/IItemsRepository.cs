using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WSEIbackendREST.Entities;

namespace WSEIbackendREST.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
        Task DeleteAllItemsAsync();
    }

    public interface ITrackRepository
    {
        Task<Track> GetItemAsync(Guid id);
        Task<IEnumerable<Track>> GetItemsAsync();
        Task CreateItemAsync(Track item);
        Task UpdateItemAsync(Track item);
        Task DeleteItemAsync(Guid id);
        Task DeleteAllItemsAsync();
    }

    public interface IEmployeeRepository
    {
        Task<Employee> GetItemAsync(Guid id);
        Task<IEnumerable<Employee>> GetItemsAsync();
        Task CreateItemAsync(Employee item);
        Task UpdateItemAsync(Employee item);
        Task DeleteItemAsync(Guid id);
        Task DeleteAllItemsAsync();
    }
}