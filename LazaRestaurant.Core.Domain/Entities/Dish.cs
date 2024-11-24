using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Domain.Entities;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Capacity { get; set; }
    public string Category { get; set; }
    
    [JsonIgnore]
    public ICollection<DishIngredient>? DishIngredients { get; set; }
    [JsonIgnore]
    public ICollection<OrderDish>? OrderDishes { get; set; }
}