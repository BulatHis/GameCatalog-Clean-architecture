namespace GameCatalogCore.DTO_s.GameDTO_s;

public class AddGameDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Year { get; set; } 
    public string Developer { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public Guid GenreId { get; set; }
    public Guid AdminReviewId { get; set; }
}