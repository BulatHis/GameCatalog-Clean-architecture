namespace GameCatalogCore.DTO_s.GenreDTO_s;

public class GetGenreByNameDtoResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}