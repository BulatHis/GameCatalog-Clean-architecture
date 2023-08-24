namespace GameCatalogCore.DTO_s.ReviewsDTO_s;

public class CreateReviewDtoRequest
{
    public string Text { get; set; } = null!;
    public int Rating { get; set; }

    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}