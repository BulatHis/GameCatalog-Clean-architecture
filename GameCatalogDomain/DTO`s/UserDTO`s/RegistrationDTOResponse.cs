namespace GameCatalogCore.DTO_s.UserDTO_s;

public class RegistrationDtoResponse
{
    public bool Result { get; set; }
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}