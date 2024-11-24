using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
    new Task DeleteAsync(Order order);
    Task UpdateAsync(Order entity, int id);

    Task SaveChanges();

    Task DeleteOrderDishes(int id);

    Task AddDishesToOrder(int orderId, List<int> dishesId);
    Task<List<Order>> GetAllWithNav();
    
    Task<Order> GetByIdWithNav(int id);
}