namespace GameCatalogCore.DTO_s.ReviewsDTO_s;

public class GetReviewByIdDtoResponse
{
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
    public string Date { get; set; } = null!;
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}