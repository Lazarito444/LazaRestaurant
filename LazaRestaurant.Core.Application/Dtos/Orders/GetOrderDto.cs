using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Dtos.Orders;

public class GetOrderDto
{
    public int Id { get; set; }
    public int TableId { get; set; }
    public double SubTotal { get; set; }
    public string State { get; set; }
    
    public List<Dish> Dishes { get; set; }
}