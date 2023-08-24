using System.Diagnostics.CodeAnalysis;

namespace GameCatalogCore.DTO_s.ReviewsDTO_s;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class DeleteReviewDtoRequest
{
    public Guid Id { get; set; }
}