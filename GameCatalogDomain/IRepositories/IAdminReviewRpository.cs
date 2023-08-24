using GameCatalogDomain.Entity;

namespace GameCatalogMsSQL.IRepositories;

public interface IAdminReviewRepository
{
    Task<AdminReview> Check(Guid id);
    Task Add(AdminReview adminReview);
    Task<AdminReview> GetByGameId(Guid gameId);
    Task Delete(AdminReview adminReview);
}