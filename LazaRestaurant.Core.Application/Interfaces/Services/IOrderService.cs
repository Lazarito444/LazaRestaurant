using LazaRestaurant.Core.Application.Dtos.Orders;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface IOrderService : IGenericService<OrderDto, Order>
{
    Task<List<OrderDto>> GetAllWithInclude();
    Task<OrderDto> GetByIdWithInclude(int id);
    Task AddDishesToOrder(int orderId, List<OrderDishDto> dishes);

    Task DeleteOrderDishes(int id);
}