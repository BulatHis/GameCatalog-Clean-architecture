using System.Diagnostics.CodeAnalysis;

namespace GameCatalogCore.DTO_s.GameDTO_s;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class AllGamesDtoResponse
{
    public List<Guid> GameId { get; set; } = null!;
    public List<string> Title { get; set; } = null!;
    public List<string> Description { get; set; } = null!;
    public List<Guid> GenreId { get; set; } = null!;
    public List<int> Year { get; set; } = null!;
    public List<string> Developer { get; set; } = null!;
    public List<string> Publisher { get; set; } = null!;
}