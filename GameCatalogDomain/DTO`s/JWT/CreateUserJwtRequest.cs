namespace GameCatalogDomain.DTO_s.JWT;

public class CreateUserJwtRequest
{
    public Guid Id { get; set; }
    public string Role { get; set; } = null!;
}