using System.Reflection;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LazaRestaurant.Core.Application;

public static class ServiceRegistration
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // AutoMapper DI
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Services DI
        services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));
        services.AddTransient<IDishService, DishService>();
        services.AddTransient<IIngredientService, IngredientService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<ITableService, TableService>();
    }
}