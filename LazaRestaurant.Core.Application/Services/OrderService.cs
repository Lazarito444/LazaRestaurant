using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Orders;
using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Services;

public class OrderService : GenericService<OrderDto, Order>, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;
    
    public OrderService(IGenericRepository<Order> repository, IMapper mapper, IOrderRepository orderRepository, IDishRepository dishRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _dishRepository = dishRepository;
    }

    public async Task<List<OrderDto>> GetAllWithInclude()
    {
        var list = await _orderRepository.GetAllWithNav();
        var result = _mapper.Map<List<OrderDto>>(list);

        return result;
    }

    public async Task<OrderDto> GetByIdWithInclude(int id)
    {
        var result = await _orderRepository.GetByIdWithNav(id);
        var orderDto = _mapper.Map<OrderDto>(result);

        return orderDto;
    }
    
    public async Task AddDishesToOrder(int orderId, List<OrderDishDto> dishes)
    {
        // var order = await _orderRepository.GetByIdWithNav(orderId);

        List<int> dishesId = new List<int>();

        foreach (var dish in dishes)
        {
            dishesId.Add(dish.DishId);
        }

        await _orderRepository.AddDishesToOrder(orderId, dishesId);
    }

    public async Task DeleteOrderDishes(int id)
    {
        await _orderRepository.DeleteOrderDishes(id);
    }
}