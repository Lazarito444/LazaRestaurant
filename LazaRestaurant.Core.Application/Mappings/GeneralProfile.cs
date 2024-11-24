using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Dishes;
using LazaRestaurant.Core.Application.Dtos.Ingredients;
using LazaRestaurant.Core.Application.Dtos.Orders;
using LazaRestaurant.Core.Application.Dtos.Tables;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        // Dishes
        CreateMap<Dish, DishDto>()
            .ReverseMap();

        CreateMap<CreateDishDto, DishDto>()
            .ForMember(x => x.DishIngredients, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.DishIngredients, opt => opt.Ignore());

        // Ingredients
        CreateMap<Ingredient, IngredientDto>()
            .ReverseMap();

        CreateMap<Ingredient, UpdateIngredientDto>()
            .ReverseMap();   
        
        CreateMap<IngredientDto, UpdateIngredientDto>()
            .ReverseMap();

        // Orders
        CreateMap<Order, OrderDto>()
            .ReverseMap();

        CreateMap<OrderDto, CreateOrderDto>()
            .ReverseMap();
        
        CreateMap<OrderDto, GetOrderDto>()
            .ReverseMap();

        CreateMap<OrderDto, OrderDishDto>()
            .ReverseMap();
        
        // Tables
        CreateMap<Table, TableDto>()
            .ReverseMap();

        CreateMap<TableDto, CreateTableDto>()
            .ReverseMap();

        CreateMap<TableDto, ChangeTableStateDto>()
            .ReverseMap(); 
        
        CreateMap<TableDto, UpdateTableDto>()
            .ReverseMap();
    }
}