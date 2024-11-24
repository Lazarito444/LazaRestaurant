using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Domain.Entities;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public ICollection<DishIngredient>? DishIngredients { get; set; }
}