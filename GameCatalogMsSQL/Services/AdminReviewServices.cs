using GameCatalogCore.DTO_s.AdminReviewDTO_s;
using GameCatalogCore.IServices;
using GameCatalogDomain.Entity;
using GameCatalogDomain.IRepositories;
using GameCatalogDomain.IServices;
using GameCatalogMsSQL.IRepositories;

namespace GameCatalogMsSQL.Services;

public class AdminReviewServices : IAdminReviewServices
{
    private readonly IAdminReviewRepository _adminReviewRepository;
    private readonly IGameRepository _gameRepository;

    public AdminReviewServices(IAdminReviewRepository adminReviewRepository, IGameRepository gameRepository )
    {
        _adminReviewRepository = adminReviewRepository;
        _gameRepository = gameRepository;
    }

    public async Task<bool> Add(AdminReviewAddDtoRequest request)
    {

        var game = await _gameRepository.Check(request.GameId);
        if (game == null)
        {
            throw new ArgumentException($"game not found");
        }

        var rating = (request.GamePlayRating * 50 / 10) +
                     (request.AddictivenessRating * 15 / 10) +
                     (request.StylizationRating * 20 / 10) +
                     (request.ReplayableRating * 15 / 10);
        var review = new AdminReview
        {
            Id = Guid.NewGuid(), GameId = request.GameId, AdminName = request.AdminName, GamePlay = request.GamePlay,
            Addictiveness = request.Addictiveness, Stylization = request.Stylization, Replayable = request.Replayable,
            Summary = request.Summary, GamePlayRating = request.GamePlayRating,
            AddictivenessRating = request.AddictivenessRating,
            StylizationRating = request.StylizationRating, ReplayableRating = request.ReplayableRating,
            Date = DateTime.Now.ToString(),
            Game = game, Rating = rating
        };
        await _adminReviewRepository.Add(review);
        return true;
    }

    public async Task<AdminReviewAddDtoResponse> GetByGameId(Guid gameId)
    {
        var game = await _gameRepository.Check(gameId);
        if (game == null)
        {
            throw new ArgumentException($"game not found");
        }

        var request = await _adminReviewRepository.GetByGameId(gameId);
        return new AdminReviewAddDtoResponse()
        {
            Id = Guid.NewGuid(), GameId = request.GameId, AdminName = request.AdminName, GamePlay = request.GamePlay,
            Addictiveness = request.Addictiveness, Stylization = request.Stylization, Replayable = request.Replayable,
            Summary = request.Summary, GamePlayRating = request.GamePlayRating,
            AddictivenessRating = request.AddictivenessRating,
            StylizationRating = request.StylizationRating, ReplayableRating = request.ReplayableRating,
            Date = DateTime.Now.ToString(), Rating = request.Rating
        };
    }

    public async Task<bool> Delete(Guid adminReviewId)
    {
        var adminReview = await _adminReviewRepository.Check(adminReviewId);
        if (adminReview == null)
        {
            throw new ArgumentException($"adminReview not found");
        }

        await _adminReviewRepository.Delete(adminReview);
        return true;
    }
}