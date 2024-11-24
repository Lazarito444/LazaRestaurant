using System.Text.Json.Serialization;

namespace LazaRestaurant.Core.Application.Dtos.Account;

public class AuthResponse
{
    
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public bool IsVerified { get; set; } = true;
    public bool HasError { get; set; }
    public string? Error { get; set; }
    public string? JWToken { get; set; }
    [JsonIgnore]
    public string? RefreshToken { get; set; }
}