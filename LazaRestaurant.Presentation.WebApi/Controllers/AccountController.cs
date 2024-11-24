using LazaRestaurant.Core.Application.Dtos.Account;
using LazaRestaurant.Core.Application.Enums;
using LazaRestaurant.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazaRestaurant.Presentation.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(AuthRequest request)
    {
        return Ok(await _accountService.AuthenticateAsync(request));
    }
    
    [HttpPost("serverRegister")]
    public async Task<IActionResult> ServerRegisterAsync(RegisterRequest request)
    {
        return Ok(await _accountService.Register(request, Roles.Server.ToString()));
    } 
    
    [Authorize(Roles = "Admin")]
    [HttpPost("adminRegister")]
    public async Task<IActionResult> AdminRegisterAsync(RegisterRequest request)
    {
        return Ok(await _accountService.Register(request, Roles.Admin.ToString()));
    }
}