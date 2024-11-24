using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Domain.Entities;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LazaRestaurant.Infrastructure.Persistence.Repositories;

public class DishRepository : GenericRepository<Dish>, IDishRepository
{
    private readonly ApplicationContext _dbContext;
    
    public DishRepository(ApplicationContext context, ApplicationContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Dish>> GetAllWithNav()
    {
        var dbSet = _dbContext.Set<Dish>()
            .Include(dish => dish.DishIngredients)
            .ThenInclude(dishIngredient => dishIngredient.Ingredient)
            .Include(dish => dish.OrderDishes)
            .ThenInclude(orderDish => orderDish.Order);

        return await dbSet.ToListAsync();
    }

    public async Task<Dish> GetByIdWithNav(int id)
    {
        var list = await GetAllWithNav();

        var dish = list.FirstOrDefault(dish => dish.Id == id);

        return dish;
    }

    public async Task AddIngredientsToDish(int dishId, List<int> ingredientsId)
    {
        var list  = await  _dbContext.DishIngredients.Where(di => di.DishId == dishId).ToListAsync();
        _dbContext.DishIngredients.RemoveRange(list);
        foreach (var ingredientId in ingredientsId)
        {
            _dbContext.DishIngredients.Add(new DishIngredient { DishId = dishId, IngredientId = ingredientId });
        }

        await _dbContext.SaveChangesAsync();
    }
}