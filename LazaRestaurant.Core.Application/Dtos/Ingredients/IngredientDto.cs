using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Dtos.Ingredients;

public class IngredientDto
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public ICollection<DishIngredient>? DishIngredients { get; set; }
}