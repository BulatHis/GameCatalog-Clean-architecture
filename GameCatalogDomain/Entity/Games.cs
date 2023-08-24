using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameCatalogDomain.Entity;

public class Game
{
    [Key] public Guid GameId { get; set; }

    [Required] [MaxLength (30)] [MinLength(1)]  public string Title { get; set; } = null!;

    [Required] [MaxLength (600)] [MinLength(50)]  public string Description { get; set; } = null!;

    [Required] public Guid GenreId { get; set; }

    [Required] [Range(1958,2050)] public int Year { get; set; }

    [Required] [MaxLength (50)] [MinLength(3)]  public string Developer { get; set; } = null!;

    [Required] [MaxLength (50)] [MinLength(3)] public string Publisher { get; set; } = null!;
    
    [Required] public string Image { get; set; } = null!;
    
    public Guid AdminReviewId { get; set; }

    [ForeignKey("GenreId")] public Genre? Genre { get; set; }
    [ForeignKey("AdminReviewId")] public AdminReview? AdminReview { get; set; }
    public List<Review> Reviews { get; set; } = null!;
}