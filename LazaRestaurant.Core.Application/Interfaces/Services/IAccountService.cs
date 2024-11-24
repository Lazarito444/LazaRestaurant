using LazaRestaurant.Core.Application.Dtos.Account;

namespace LazaRestaurant.Core.Application.Interfaces.Services;

public interface IAccountService
{
    Task<AuthResponse> AuthenticateAsync(AuthRequest request);
    Task<RegisterResponse> Register(RegisterRequest request, string role);
    Task SignOutAsync();
}