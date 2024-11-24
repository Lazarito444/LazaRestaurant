using LazaRestaurant.Core.Application.Dtos.Dishes;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface IDishService : IGenericService<DishDto, Dish>
{
    Task<List<DishDto>> GetAllWithInclude();

    Task<DishDto> GetByIdWithInclude(int id);

    Task UpdateIngredients(int dishId, List<DishIngredientDto> dishIngredientDtos);
}