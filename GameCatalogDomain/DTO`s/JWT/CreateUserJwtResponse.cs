namespace GameCatalogCore.DTO_s.JWT;

public class CreateUserJwtResponse
{
    public bool Success { get; set; } 
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}