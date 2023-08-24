namespace GameCatalogCore.DTO_s.ReviewsDTO_s;

public class UpdateTextReviewDtoRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
}