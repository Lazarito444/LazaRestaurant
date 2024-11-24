using System.ComponentModel;
using System.Text.Json.Serialization;
using LazaRestaurant.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace LazaRestaurant.Core.Application.Dtos.Tables;

public class TableDto
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    
    [JsonIgnore]
    public ICollection<Order>? Orders { get; set; }
}