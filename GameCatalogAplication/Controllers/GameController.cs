using GameCatalogCore.DTO_s.GameDTO_s;
using GameCatalogCore.Filters;
using GameCatalogDomain.DTO_s.GameDTO_s;
using GameCatalogDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAplication.Controllers;

[ApiController]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly IGameServices _gameServices;
    
    public GameController(ILogger<GameController> logger, IGameServices gameServices)
    {
        _logger = logger;
        _gameServices = gameServices;
    }

    /// <summary>
    /// Показывает все игры(пока не используется)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">ошибка при выводе игр</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getallgames")]
    public async Task<AllGamesDtoResponse> GetAllGames()
    {
        _logger.IsEnabled(LogLevel.Warning);
        return await _gameServices.GetAllGames();
    }

    /// <summary>
    /// Ищем игру по id (нужно при выводе страницы игры)
    /// </summary>
    /// <response code="200">Игра найдена</response>
    /// <response code="500">ошибка при поиске игры(скорее всего ее нет)</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getgamebyid")]
    public async Task<GetGameByIdResponse> GetGameById(Guid id)
    {
        return await _gameServices.GetGameById(id);
    }

    /// <summary>
    /// Получение всех названий игр(используется на главной странице + достает все пути к картинкам на сервере)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при выводе игр</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getalltitles")]
    public async Task<AllTitlesAndImgDtoResponse> GetAllTitlesAndImg()
    {
        return await _gameServices.GetAllTitles();
    }
    
    /// <summary>
    ///  Ищем игру по названию (нужно при добавление нового обзора от поль-ля)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при поиске игры (скорее всего такой нет)</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getgamebytitle")]
    public async Task<GetGameByIdResponse> GetGameByTitle(string title)
    {
        return await _gameServices.CheckByTitle(title);
    }
    
    /// <summary>
    ///  Добовляем игру (используется только админом)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при создание игры</response>
    [HttpPost]
    [ExecutionTime]
    [Route("addgame")]
    [CustomExceptionFilter]
    [Authorize("Admin")]
    public async Task<bool> Addgame(AddGameDto request)
    {
        return await _gameServices.Add(request);
    }
}