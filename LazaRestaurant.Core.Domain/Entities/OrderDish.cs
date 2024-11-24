namespace LazaRestaurant.Core.Domain.Entities;

public class OrderDish
{
    public int? DishId { get; set; }
    public int? OrderId { get; set; }
    public Dish? Dish { get; set; }
    public Order? Order { get; set; }
}