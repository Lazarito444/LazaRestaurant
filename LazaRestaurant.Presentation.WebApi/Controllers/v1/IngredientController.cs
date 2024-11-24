using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Ingredients;
using LazaRestaurant.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazaRestaurant.Presentation.WebApi.Controllers.v1;

//[Authorize(Roles = "Admin")]
public class IngredientController : BaseApiController
{
    private readonly IIngredientService _ingredientService;
    private readonly IMapper _mapper;

    public IngredientController(IIngredientService ingredientService, IMapper mapper)
    {
        _ingredientService = ingredientService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(IngredientDto ingredientDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _ingredientService.Add(ingredientDto);
            return CreatedAtAction(nameof(GetById), new {Id = result.Id}, result);
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
    public async Task<IActionResult> Update(int id, UpdateIngredientDto updateIngredientDto)
    {
        
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            var ingredientDto = await _ingredientService.GetByIdWithInclude(id);
            ingredientDto.Name = updateIngredientDto.Name;
            
            await _ingredientService.Update(ingredientDto, id);
            return Ok(await _ingredientService.GetById(id));
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
            var list = await _ingredientService.GetAllWithInclude();

            if (list.Count == 0 || list == null) return NoContent();

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
            var result = await _ingredientService.GetByIdWithInclude(id);

            if (result == null) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}