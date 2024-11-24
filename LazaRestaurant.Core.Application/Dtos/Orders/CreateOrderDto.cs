using System.ComponentModel.DataAnnotations;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Dtos.Orders;

public class CreateOrderDto
{
    [Required]
    public int TableId { get; set; }
    [Required]
    public double SubTotal { get; set; }
    public string State { get; set; } = "En proceso";
}