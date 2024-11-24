using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using LazaRestaurant.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LazaRestaurant.Infrastructure.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        
        // Contexts
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging();

        });
        
        // Repositories Registration (DI)
         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IDishRepository, DishRepository>();
        services.AddTransient<IIngredientRepository, IngredientRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<ITableRepository, TableRepository>();
    }
}