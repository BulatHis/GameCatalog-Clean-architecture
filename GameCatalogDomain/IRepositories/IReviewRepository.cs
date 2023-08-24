using GameCatalogDomain.Entity;

namespace GameCatalogDomain.IRepositories;

public interface IReviewRepository
{
    Task<Review> Check(Guid id);
    Task<bool> Add(Review review);
    Task<bool> Update(Review review);
    Task<List<Review>> GetByGameId(Guid gameId);
    Task<bool> Delete(Review review);
}