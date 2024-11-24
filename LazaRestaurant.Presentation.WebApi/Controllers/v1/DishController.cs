using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Dishes;
using LazaRestaurant.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazaRestaurant.Presentation.WebApi.Controllers.v1;

[Authorize(Roles = "Admin")]
public class DishController : BaseApiController
{
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public DishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateDishDto createDishDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            DishDto dishDto = _mapper.Map<DishDto>(createDishDto);
            
            var result = await _dishService.Add(dishDto);

            if (createDishDto.DishIngredients != null)
            {
                await _dishService.UpdateIngredients(result.Id, createDishDto.DishIngredients.ToList());
               result = await _dishService.GetByIdWithInclude(result.Id);
            }
            return CreatedAtAction(nameof(GetById), new {Id = result.Id} ,result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, CreateDishDto createDishDto)
    {
        try
        {
            if (id <= 0 || !ModelState.IsValid) return BadRequest();

            DishDto dishDto = _mapper.Map<DishDto>(createDishDto);
            dishDto.Id = id;
            await _dishService.Update(dishDto, id);
            if (createDishDto.DishIngredients != null)
            {
                await _dishService.UpdateIngredients(id, createDishDto.DishIngredients.ToList());
            }

            dishDto = await _dishService.GetByIdWithInclude(id);
            return Ok(dishDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> List()
    {
        try
        {
            var list = await _dishService.GetAllWithInclude();
            if (list == null || list.Count == 0) return NoContent();

            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)    
    {
        try
        {
            var result = await _dishService.GetByIdWithInclude(id);

            if (result == null) return NoContent();

            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}