namespace GameCatalogCore.DTO_s.ReviewsDTO_s;

public class GetReviewByGameIdDtoResponse
{
    public List<Guid> Id { get; set; } = null!;
    public List<string> Text { get; set; } = null!;
    public List<int> Rating { get; set; } = null!;
    public List<string> Date { get; set; } = null!;
    public List<Guid> UserId { get; set; } = null!;
    public List<Guid> GameId { get; set; } = null!;
}