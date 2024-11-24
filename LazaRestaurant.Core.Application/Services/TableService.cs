using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Tables;
using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Services;

public class TableService : GenericService<TableDto, Table>, ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;
    
    public TableService(IGenericRepository<Table> repository, IMapper mapper, ITableRepository tableRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _tableRepository = tableRepository;
    }

    public async Task<List<TableDto>> GetAllWithInclude()
    {
        var list = await _tableRepository.GetAllWithNav();

        var result = _mapper.Map<List<TableDto>>(list);

        return result;
    }

    public async Task<TableDto> GetByIdWithInclude(int id)
    {
        var result = await _tableRepository.GetByIdWithNav(id);
        var tableDto = _mapper.Map<TableDto>(result);

        return tableDto;
    }
}