using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Domain.Entities;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LazaRestaurant.Infrastructure.Persistence.Repositories;

public class TableRepository : GenericRepository<Table>, ITableRepository
{
    private readonly ApplicationContext _dbContext;
    
    public TableRepository(ApplicationContext context, ApplicationContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Table>> GetAllWithNav()
    {
        var dbSet = _dbContext.Set<Table>()
            .Include(table => table.Orders);

        return await dbSet.ToListAsync();
    }

    public async Task<Table> GetByIdWithNav(int id)
    {
        var list = await GetAllWithNav();

        var table = list.FirstOrDefault(table => table.Id == id);

        return table;
    }
}