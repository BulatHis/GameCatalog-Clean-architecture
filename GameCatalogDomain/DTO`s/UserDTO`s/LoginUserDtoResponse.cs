namespace GameCatalogCore.DTO_s.UserDTO_s;

public class LoginUserDtoResponse
{
    public Guid UserId { get; set; }
    public bool Result { get; set; }
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    
    public  bool IsConfirmed { get; set; }
}