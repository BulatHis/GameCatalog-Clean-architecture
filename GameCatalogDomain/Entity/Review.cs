using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogDomain.Entity;

public class Review
{
    [Key] public Guid Id { get; set; }
    [Required] [MaxLength (500)] [MinLength(1)]  public string Text { get; set; } = null!;
    [Required] [Range(1,5)]  public int Rating { get; set; }
    [Required] public string Date { get; set; } = null!;
    [Required] public Guid UserId { get; set; }
    [Required] public Guid GameId { get; set; }

    [ForeignKey("UserId")] public User? User { get; set; }
    [ForeignKey("GameId")] public Game? Game { get; set; }
}