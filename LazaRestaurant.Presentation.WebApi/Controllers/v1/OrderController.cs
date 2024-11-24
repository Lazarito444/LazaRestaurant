using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Dishes;
using LazaRestaurant.Core.Application.Dtos.Orders;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazaRestaurant.Presentation.WebApi.Controllers.v1;

[Authorize(Roles = "Server")]
public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            if (createOrderDto.State.ToLower() != "en proceso" && createOrderDto.State.ToLower() != "completada") return BadRequest();

            var orderDto = _mapper.Map<OrderDto>(createOrderDto);
            var result = await _orderService.Add(orderDto);
            result = await _orderService.GetByIdWithInclude(result.Id);
            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, List<OrderDishDto> dishes)
    {
        try
        {
            await _orderService.AddDishesToOrder(id, dishes);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List()
    {
        try
        {
            var list = await _orderService.GetAllWithInclude();
            if (list == null) return NoContent();
            var newList = new List<GetOrderDto>();

            foreach (var order in list)
            {
                GetOrderDto getOrderDto = _mapper.Map<GetOrderDto>(order);
                getOrderDto.Dishes = new List<Dish>();
                
                foreach (var orderDish in order.OrderDishes)
                {
                    getOrderDto.Dishes.Add(new Dish
                    {
                        Id = orderDish.Dish.Id,
                        Name = orderDish.Dish.Name,
                        Capacity = orderDish.Dish.Capacity,
                        Category = orderDish.Dish.Category,
                        Price = orderDish.Dish.Price
                    });
                }
                newList.Add(getOrderDto);
            }
            
            
            return Ok(newList);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _orderService.GetByIdWithInclude(id);

            if (result == null) return NoContent();

            var newResult = _mapper.Map<GetOrderDto>(result);
            newResult.Dishes = new List<Dish>();
            if (result.OrderDishes != null)
            {
                foreach (var orderDish in result.OrderDishes)
                {
                    newResult.Dishes.Add(new Dish
                    {
                        Id = orderDish.Dish.Id,
                        Name = orderDish.Dish.Name,
                        Capacity = orderDish.Dish.Capacity,
                        Category = orderDish.Dish.Category,
                        Price = orderDish.Dish.Price
                    });
                }
            }
            else
            {
                newResult.Dishes = null;
            }
 

            return Ok(newResult);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _orderService.GetById(id);
            await _orderService.DeleteOrderDishes(id);
            await _orderService.Delete(result);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}