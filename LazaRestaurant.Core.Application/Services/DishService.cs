using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Dishes;
using LazaRestaurant.Core.Application.Interfaces.Repositories;
using LazaRestaurant.Core.Application.Interfaces.Services;
using LazaRestaurant.Core.Domain.Entities;

namespace LazaRestaurant.Core.Application.Services;

public class DishService : GenericService<DishDto, Dish>, IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;
    
    public DishService(IGenericRepository<Dish> repository, IMapper mapper, IDishRepository dishRepository) : base(repository, mapper)
    {
        _mapper = mapper;
        _dishRepository = dishRepository;
    }

    public async Task<List<DishDto>> GetAllWithInclude()
    {
        var list = await _dishRepository.GetAllWithNav();

        var result = _mapper.Map<List<DishDto>>(list);

        return result;
    }

    public async Task<DishDto> GetByIdWithInclude(int id)
    {
        var result = await _dishRepository.GetByIdWithNav(id);
        var dishDto = _mapper.Map<DishDto>(result);

        return dishDto;
    }

    public async Task UpdateIngredients(int dishId, List<DishIngredientDto> dishIngredientDtos)
    {
        List<int> dishIngredientsId = new List<int>();

        foreach (var dishIngredientDto in dishIngredientDtos)
        {
            dishIngredientsId.Add(dishIngredientDto.IngredientId);
        }

        await _dishRepository.AddIngredientsToDish(dishId, dishIngredientsId);
    }
}
