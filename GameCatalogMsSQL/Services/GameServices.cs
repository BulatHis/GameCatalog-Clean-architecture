using GameCatalogCore.DTO_s.GameDTO_s;
using GameCatalogDomain.DTO_s.GameDTO_s;
using GameCatalogDomain.Entity;
using GameCatalogDomain.IRepositories;
using GameCatalogDomain.IServices;
using GameCatalogMsSQL.IRepositories;

namespace GameCatalogMsSQL.Services;

public class GameServices : IGameServices
{
    private readonly IGameRepository _gameRepository;
    private readonly IAdminReviewRepository _adminReviewRepository;

    public GameServices(IGameRepository gameRepository, IAdminReviewRepository adminReviewRepository)
    {
        _gameRepository = gameRepository;
        _adminReviewRepository = adminReviewRepository;
    }
    

    public async Task<AllGamesDtoResponse> GetAllGames()
    {
        var games = await _gameRepository.GetAll();
        if (games == null)
        {
            throw new ArgumentException($"no Games :(");
        }

        return new AllGamesDtoResponse()
        {
            GameId = games.Select(g => g.GameId).ToList(),
            Title = games.Select(g => g.Title).ToList(),
            Description = games.Select(g => g.Description).ToList(),
            GenreId = games.Select(g => g.GenreId).ToList(),
            Year = games.Select(g => g.Year).ToList(),
            Developer = games.Select(g => g.Developer).ToList(),
            Publisher = games.Select(g => g.Publisher).ToList()
        };
    }

    public async Task<GetGameByIdResponse> GetGameById(Guid id)
    {
        var game = await _gameRepository.Check(id);
        if (game == null)
        {
            throw new ArgumentException($"no Game :(");
        }

        return new GetGameByIdResponse()
        {
            GameId = game.GameId,
            Title = game.Title,
            Description = game.Description,
            GenreId = game.GenreId,
            Year = game.Year,
            Developer = game.Developer,
            Publisher = game.Publisher,
            AdminReviewId = game.AdminReviewId
            
        };
    }

    public async Task<AllTitlesAndImgDtoResponse> GetAllTitles()
    {
        var titles = await _gameRepository.GetTitles();
        var images = await _gameRepository.GetImages();
        if (titles == null || images ==null)
        {
            throw new ArgumentException($"no Games :(");
        }

        return new AllTitlesAndImgDtoResponse()
        {
            Titles = titles, Images = images
        };
    }

    public async Task<bool> Delete(Guid id)
    {
        var game = await _gameRepository.Check(id);
        if (game == null)
        {
            throw new ArgumentException("Game not found");
        }
        await _gameRepository.Delete(game);
        return true;
    }

    public async Task<bool> Add(AddGameDto request)
    {
        var user = new Game()
        {
            GameId = Guid.NewGuid(),Title = request.Title, Description = request.Description,Year = request.Year,Developer = request.Developer, Publisher = request.Publisher
            ,GenreId = request.GenreId, AdminReviewId = request.AdminReviewId
        };
        await _gameRepository.Add(user);
        return true;
    }

    public async Task<GetGameByIdResponse> CheckByTitle(string name)
    {
        var game = await _gameRepository.CheckByTitle(name);
        if (game == null)
        {
            throw new ArgumentException($"no Game :(");
        }

        return new GetGameByIdResponse()
        {
            GameId = game.GameId,
            Title = game.Title,
            Description = game.Description,
            GenreId = game.GenreId,
            Year = game.Year,
            Developer = game.Developer,
            Publisher = game.Publisher,
            AdminReviewId = game.AdminReviewId
            
        };
    }
}