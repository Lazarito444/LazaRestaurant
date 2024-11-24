using System.Text.Json.Serialization;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Dtos.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public double SubTotal { get; set; }
    public string State { get; set; } = "En proceso";
    
    public Table? Table { get; set; }
    
    [JsonIgnore]
    public ICollection<OrderDish>? OrderDishes { get; set; }
}