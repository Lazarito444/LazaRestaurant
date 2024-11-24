namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface IGenericService<EntityDto, Entity>
    where EntityDto : class
    where Entity : class
{
    Task Update(EntityDto entityDto, int id);

    Task<EntityDto> Add(EntityDto entityDto);

    Task Delete(EntityDto entityDto);

    Task<EntityDto> GetById(int id);

    Task<List<EntityDto>> GetAll();
}