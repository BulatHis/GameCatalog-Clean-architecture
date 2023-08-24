using GameCatalogCore.DTO_s.AdminReviewDTO_s;

namespace GameCatalogDomain.IServices;

public interface IAdminReviewServices
{
    Task<bool> Add(AdminReviewAddDtoRequest request);
    Task<AdminReviewAddDtoResponse> GetByGameId(Guid gameId);
    
    Task<bool> Delete(Guid adminReviewId);
}