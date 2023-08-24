using System.ComponentModel.DataAnnotations;

namespace GameCatalogDomain.Entity;

public class Genre
{
    public Guid Id { get; set; }
    [MaxLength (20)] [MinLength(3)]  public string Title { get; set; } = null!;
    [MaxLength (500)] [MinLength(10)]  public string Description { get; set; } = null!;
    public List<Game> Games { get; set; } = null!;
}