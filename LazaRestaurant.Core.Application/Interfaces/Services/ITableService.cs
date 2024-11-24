using LazaRestaurant.Core.Application.Dtos.Tables;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface ITableService : IGenericService<TableDto, Table>
{
    Task<List<TableDto>> GetAllWithInclude();

    Task<TableDto> GetByIdWithInclude(int id);
}