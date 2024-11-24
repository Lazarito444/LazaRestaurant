using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Domain.Entities;

public class DishIngredient
{
    [JsonIgnore]
    public int? DishId { get; set; }
    [JsonIgnore]
    public int? IngredientId { get; set; }
    [JsonIgnore]
    public Dish? Dish { get; set; }
    public Ingredient? Ingredient { get; set; }
}