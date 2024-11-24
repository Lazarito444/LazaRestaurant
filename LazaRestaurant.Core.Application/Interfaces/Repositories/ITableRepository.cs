using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Repositories;

public interface ITableRepository : IGenericRepository<Table>
{
    Task<List<Table>> GetAllWithNav();
    
    Task<Table> GetByIdWithNav(int id);
}