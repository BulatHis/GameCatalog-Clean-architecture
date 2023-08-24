using System.ComponentModel.DataAnnotations;

namespace GameCatalogDomain.DTO_s.UserDTO_s;

public class RegistrationDtoRequest
{ 
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;
}