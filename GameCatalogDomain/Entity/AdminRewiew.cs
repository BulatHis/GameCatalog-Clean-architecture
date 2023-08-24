using System.ComponentModel.DataAnnotations;

namespace GameCatalogDomain.Entity;

public class AdminReview
{
    [Key] public Guid Id { get; set; }
    
    [Required] [MaxLength (500)] [MinLength(10)]  public string Summary { get; set; } = null!;

    [Required] [MaxLength (500)] [MinLength(10)] public string GamePlay { get; set; } = null!;

    [Required] [MaxLength (500)] [MinLength(10)] public string Addictiveness { get; set; } = null!;
    
    [Required] [MaxLength(500)] public string Stylization { get; set; } = null!;
    
    [Required] [MaxLength (500)] [MinLength(10)] public string Replayable { get; set; } = null!;
    [Required] [Range(0,100)]  public double Rating { get; set; }
    
    [Required] [Range(0,10)] public int GamePlayRating { get; set; }
    
    [Required] [Range(0,10)] public int AddictivenessRating { get; set; }
    
    [Required] [Range(0,10)] public int StylizationRating { get; set; }
    
    [Required] [Range(0,10)]  public int ReplayableRating { get; set; }
    [Required]  public string Date { get; set; } = null!;
    [Required] [MaxLength (20)] [MinLength(3)] public string AdminName { get; set; } = null!;
    [Required] public Guid GameId { get; set; }
    public Game? Game { get; set; }
}