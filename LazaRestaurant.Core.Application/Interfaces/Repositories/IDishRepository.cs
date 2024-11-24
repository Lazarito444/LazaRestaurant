using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Repositories;

public interface IDishRepository : IGenericRepository<Dish>
{
    Task<List<Dish>> GetAllWithNav();
    
    Task<Dish> GetByIdWithNav(int id);

    Task AddIngredientsToDish(int dishId, List<int> ingredientsId);
}