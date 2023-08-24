using GameCatalogCore.DTO_s.ReviewsDTO_s;

namespace GameCatalogCore.IServices;

public interface IReviewServices
{
    Task<CreateReviewDtoResponse> AddReview(CreateReviewDtoRequest request);
    Task<UpdateTextReviewDtoResponse> UpdateReviewText(UpdateTextReviewDtoRequest request);
    Task<GetReviewByGameIdDtoResponse> GetReviewByGameId(GetReviewByGameIdDtoRequest request);
    Task<GetReviewByIdDtoResponse> GetReviewById(GetReviewByIdDtoRequest request);
    Task<DeleteReviewDtoResponse> DeleteReview(DeleteReviewDtoRequest request);
}