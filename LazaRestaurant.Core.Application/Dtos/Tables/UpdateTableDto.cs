using System.ComponentModel.DataAnnotations;

namespace LazaRestaurant.Core.Application.Dtos.Tables;

public class UpdateTableDto
{
    [Required]
    public string Description { get; set; }
    [Required]
    public int Capacity { get; set; }
}