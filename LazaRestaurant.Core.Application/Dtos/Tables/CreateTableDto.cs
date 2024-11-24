using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace LazaRestaurant.Core.Application.Dtos.Tables;

public class CreateTableDto
{
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    
    [Required]
    public int Capacity { get; set; }
    
    [Required]
    public string Description { get; set; }

    public string State { get; set; } = "Disponible";
}