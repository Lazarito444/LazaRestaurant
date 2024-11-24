using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int? TableId { get; set; }
    public double SubTotal { get; set; }
    public string State { get; set; }
    
    [JsonIgnore]
    public Table? Table { get; set; }
    [JsonIgnore]
    public ICollection<OrderDish>? OrderDishes { get; set; }
}