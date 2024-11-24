using LazaRestaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace LazaRestaurant.Core.Application.Dtos.Dishes;

public class CreateDishDto
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Capacity { get; set; }
    public string Category { get; set; }
    
    public ICollection<DishIngredientDto>? DishIngredients { get; set; }
}