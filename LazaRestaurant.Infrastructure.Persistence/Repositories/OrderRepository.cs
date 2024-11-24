using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Domain.Entities;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LazaRestaurant.Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly ApplicationContext _dbContext;
    
    public OrderRepository(ApplicationContext context, ApplicationContext dbContext) : base(context)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChanges()
    {
        _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order, int id)
    {
        _dbContext.Update(order);
        _dbContext.SaveChangesAsync();
    }
    
    public async Task<List<Order>> GetAllWithNav()
    {
        var dbSet = _dbContext.Set<Order>()
            .Include(order => order.Table)
            .Include(order => order.OrderDishes)
            .ThenInclude(orderDish => orderDish.Dish);

        return await dbSet.ToListAsync();
    }

    public async Task<Order> GetByIdWithNav(int id)
    {
        var list = await GetAllWithNav();

        var order = list.FirstOrDefault(order => order.Id == id);

        return order;
    }

    public async Task AddDishesToOrder(int orderId, List<int> dishesId)
    {
        var existingOrderDishes = await _dbContext.OrderDishes.Where(od => od.OrderId == orderId).ToListAsync();

        if (existingOrderDishes.Count > 0)
        {
            _dbContext.OrderDishes.RemoveRange(existingOrderDishes);
            await _dbContext.SaveChangesAsync();
        }

        foreach (int dishId in dishesId)
        {
            _dbContext.OrderDishes.Add(new OrderDish { OrderId = orderId, DishId = dishId });
        }
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderDishes(int id)
    {
        var orderDishes = await _dbContext.OrderDishes.Where(od => od.OrderId == id).ToListAsync();
        _dbContext.OrderDishes.RemoveRange(orderDishes);
        await _dbContext.SaveChangesAsync();
    }
}