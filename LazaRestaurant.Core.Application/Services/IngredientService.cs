using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Ingredients;
using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Services;

public class IngredientService : GenericService<IngredientDto, Ingredient>, IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IMapper _mapper;
    
    public IngredientService(IGenericRepository<Ingredient> repository, IMapper mapper, IIngredientRepository ingredientRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<List<IngredientDto>> GetAllWithInclude()
    {
        var list = await _ingredientRepository.GetAllWithNav();
        var result = _mapper.Map<List<IngredientDto>>(list);

        return result;
    }

    public async Task<IngredientDto> GetByIdWithInclude(int id)
    {
        var result = await _ingredientRepository.GetByIdWithNav(id);
        var ingredientDto = _mapper.Map<IngredientDto>(result);

        return ingredientDto;
    }
}