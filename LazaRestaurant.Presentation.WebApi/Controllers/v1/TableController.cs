using AutoMapper;
using LazaRestaurant.Core.Application.Dtos.Tables;
using LazaRestaurant.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LazaRestaurant.Presentation.WebApi.Controllers.v1;

public class TableController : BaseApiController
{
    private readonly ITableService _tableService;
    private readonly IMapper _mapper;

    public TableController(ITableService tableService, IMapper mapper)
    {
        _tableService = tableService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(CreateTableDto createTableDto)
    {
        try
        {
            
            
            if (createTableDto.State == String.Empty || createTableDto.State == null)
            {
                createTableDto.State = "Disponible";
            }
            
            string state = createTableDto.State.ToLower();
            if (!(state == "disponible" || state == "atendida" || state == "en proceso"))
            {
                return BadRequest(new {Message = "Ingrese uno de los siguientes estados: Disponible, En proceso, Atendida"});
            }
            
            var tableDto = _mapper.Map<TableDto>(createTableDto);
            
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tableService.Add(tableDto);

            return CreatedAtAction(nameof(GetById), new { Id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, UpdateTableDto updateTableDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            TableDto table = await _tableService.GetByIdWithInclude(id);

            table.Description = updateTableDto.Description;
            table.Capacity = updateTableDto.Capacity;

            await _tableService.Update(table, id);

            return Ok(table); 
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Admin,Server")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] 
    public async Task<IActionResult> List()
    {
        try
        {
            var list = await _tableService.GetAllWithInclude();

            if (list == null || list.Count == 0) return NoContent();

            return Ok(list);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        } 
    }
    
    [Authorize(Roles = "Admin,Server")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _tableService.GetByIdWithInclude(id);

            if (result == null) return NoContent();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Server")]
    [HttpGet("TableOrder/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTableOrder(int id)
    {
        try
        {
            var table = await _tableService.GetByIdWithInclude(id);
            var orders = table.Orders.Where(order => order.State.ToLower() == "en proceso").ToList();

            if (orders == null || orders.Count == 0) return NoContent();

            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "Server")]
    [HttpPatch("State/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ChangeState(int id, [FromBody] ChangeTableStateDto changeTableStateDto)
    {
        try
        {
            string state = changeTableStateDto.State.ToLower();
            if (state != "disponible" && state != "en proceso" && state != "atendida") throw new Exception();
            
            var table = await _tableService.GetByIdWithInclude(id);

            if (table == null) return NotFound();

            table.State = changeTableStateDto.State;

            await _tableService.Update(table, id);
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}