using WSEIbackendREST.Dtos;
using WSEIbackendREST.Entities;

namespace WSEIbackendREST
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }

        public static TrackDto AsDto(this Track item)
        {
            return new TrackDto
            {
                Id = item.Id,
                Name = item.Name,
                Length = item.Length,
                CreatedDate = item.CreatedDate
            };
        }

        public static EmployeeDto AsDto(this Employee item)
        {
            return new EmployeeDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Pesel = item.Pesel,
                CreatedDate = item.CreatedDate
            };
        }
    }
}