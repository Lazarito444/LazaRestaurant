using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Domain.Entities;

public class Table
{
    public int Id { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    
    [JsonIgnore]
    public ICollection<Order>? Orders { get; set; }
}