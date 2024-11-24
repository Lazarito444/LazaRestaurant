using System.Text.Json.Serialization;
using LazaRestaurant.Core.Application;
using LazaRestaurant.Infrastructure.Identity;
using LazaRestaurant.Infrastructure.Identity.Contexts;
using LazaRestaurant.Infrastructure.Identity.Entities;
using LazaRestaurant.Infrastructure.Identity.Seeds;
using LazaRestaurant.Infrastructure.Persistence;
using LazaRestaurant.Infrastructure.Persistence.Contexts;
using LazaRestaurant.Presentation.WebApi.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.EnableAnnotations());
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddIdentityLayer(builder.Configuration);
builder.Services.AddApplicationLayer();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    try
    {
        var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        
        applicationContext.Database.Migrate();
        identityContext.Database.Migrate();

        
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(roleManager);
        await DefaultSuperAdmin.SeedAsync(userManager);
        await DefaultAdmin.SeedAsync(userManager);
        await DefaultServer.SeedAsync(userManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSwaggerExtension();
app.MapControllers();

app.Run();