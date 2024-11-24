using System.Text.Json.Serialization;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Dtos.Ingredients;

public class UpdateIngredientDto
{
    public string Name { get; set; }
}