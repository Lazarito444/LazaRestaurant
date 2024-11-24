using LazaRestaurant.Core.Application.Dtos;
using LazaRestaurant.Core.Application.Dtos.Ingredients;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface IIngredientService : IGenericService<IngredientDto, Ingredient>
{
    Task<List<IngredientDto>> GetAllWithInclude();

    Task<IngredientDto> GetByIdWithInclude(int id);
}