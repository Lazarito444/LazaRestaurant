using AutoMapper;
using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Application.Interfaces.Services;

namespace LazaRestaurant.Core.Application.Services;

public class GenericService<EntityDto, Entity> : IGenericService<EntityDto, Entity>
    where EntityDto : class
    where Entity : class
{
    private readonly IGenericRepository<Entity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Update(EntityDto entityDto, int id)
    {
        Entity entity = _mapper.Map<Entity>(entityDto);
        await _repository.UpdateAsync(entity, id);
    }

    public async Task<EntityDto> Add(EntityDto entityDto)
    {
        Entity entity = _mapper.Map<Entity>(entityDto);
        entity = await _repository.AddAsync(entity);
        
        entityDto = _mapper.Map<EntityDto>(entity);
        return entityDto;
    }

    public async Task Delete(EntityDto entityDto)
    {
        Entity entity = _mapper.Map<Entity>(entityDto);
        await _repository.DeleteAsync(entity);    
    }
    
    public async Task<EntityDto> GetById(int id)
    {
        Entity entity = await _repository.GetByIdAsync(id);
        EntityDto entityDto = _mapper.Map<EntityDto>(entity);

        return entityDto;
    }

    public async Task<List<EntityDto>> GetAll()
    {
        List<Entity> entities = await _repository.GetAllAsync();
        List<EntityDto> entityDtos = _mapper.Map<List<EntityDto>>(entities);

        return entityDtos;
    }
}