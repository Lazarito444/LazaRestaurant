using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Domain.Entities;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LazaRestaurant.Infrastructure.Persistence.Repositories;

public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    private readonly ApplicationContext _dbContext;
    
    public IngredientRepository(ApplicationContext context, ApplicationContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Ingredient>> GetAllWithNav()
    {
        var dbSet = _dbContext.Set<Ingredient>()
            .Include(ingredient => ingredient.DishIngredients)
            .ThenInclude(dishIngredient => dishIngredient.Dish);

        return await dbSet.ToListAsync();
    }

    public async Task<Ingredient> GetByIdWithNav(int id)
    {
        var list = await GetAllWithNav();

        var ingredient = list.FirstOrDefault(ingredient => ingredient.Id == id);

        return ingredient;
    }
}