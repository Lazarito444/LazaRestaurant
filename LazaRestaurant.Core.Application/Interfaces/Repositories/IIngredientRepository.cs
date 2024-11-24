using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Repositories;

public interface IIngredientRepository : IGenericRepository<Ingredient>
{
    Task<List<Ingredient>> GetAllWithNav();
    
    Task<Ingredient> GetByIdWithNav(int id);
}