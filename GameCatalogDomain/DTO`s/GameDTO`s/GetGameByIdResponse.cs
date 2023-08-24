using System.Diagnostics.CodeAnalysis;
using GameCatalogDomain.Entity;

namespace GameCatalogCore.DTO_s.GameDTO_s;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class GetGameByIdResponse
{
    public Guid GameId{  get; set; }
    public string Title{  get; set; } = null!;
    public string Description{  get; set; }= null!;
    public Guid GenreId{  get; set; }
    public int Year{  get; set; } 
    public string Developer{  get; set; } = null!;
    public string Publisher{  get; set; } = null!;
    public Guid  AdminReviewId{  get; set; }
}