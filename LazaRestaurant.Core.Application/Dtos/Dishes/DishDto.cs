using System.Text.Json.Serialization;
using LazaRestaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace LazaRestaurant.Core.Application.Dtos.Dishes;

public class DishDto
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Capacity { get; set; }
    public string Category { get; set; }
    
    public ICollection<DishIngredient>? DishIngredients { get; set; }
    
    [JsonIgnore]
    public ICollection<OrderDish>? OrderDishes { get; set; }
}