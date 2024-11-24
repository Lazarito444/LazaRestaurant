using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LazaRestaurant.Infrastructure.Persistence.Repositories;

public class GenericRepository<Entity> : IGenericRepository<Entity>
    where Entity : class
{
    private readonly ApplicationContext _dbContext;

    public GenericRepository(ApplicationContext context)
    {
        _dbContext = context;
    }

    public async Task<Entity> AddAsync(Entity entity)
    {
        await _dbContext.Set<Entity>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Entity entity, int id)
    {
        _dbContext.Set<Entity>().Update(entity);
        
        // var entry = await _dbContext.Set<Entity>().FindAsync(id);
        // _dbContext.Entry(entry).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(Entity entity)
    {
        try
        {
            _dbContext.Set<Entity>().Remove(entity);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine(ex.Message);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Entity> GetByIdAsync(int id)
    {
        return await _dbContext.Set<Entity>().FindAsync(id);
    }

    public async Task<List<Entity>> GetAllAsync()
    {
        return await _dbContext.Set<Entity>().ToListAsync();
    }
}