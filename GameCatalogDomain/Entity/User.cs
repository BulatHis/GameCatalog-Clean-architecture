using System.ComponentModel.DataAnnotations;
// ReSharper disable All

namespace GameCatalogDomain.Entity;

public class User 
{
    [Key] public Guid Id { get; set; }
    [Required] [MaxLength (13)] [MinLength(1)] public string Name { get; set; } = null!;
    [Required] [MaxLength (15)] [MinLength(5)] public string Password { get; set; } = null!;
    [Required] [MaxLength (30)] [MinLength(6)] public string Email { get; set; } = null!;
    [Required]   public bool IsConfirmed { get; set; }
    
    [Required]   public string Role { get; set; } = null!;
    
    [Required]   public string RefreshToken { get; set; } = null!;
    [Required]  public string SecretKey { get; set; } = null!;
    public List<Review> Reviews { get; set; } = null!;
}