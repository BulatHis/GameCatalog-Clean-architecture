using GameCatalogCore.DTO_s.GenreDTO_s;
using GameCatalogCore.Filters;
using GameCatalogCore.IServices;
using GameCatalogDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAplication.Controllers;

[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreServices _genreServices;

    public GenreController(IGenreServices genreServices)
    {
        _genreServices = genreServices;
    }
    
    /// <summary>
    /// Берем жанр по названию (пока не используется - план был в поиске по жанрам на сайте)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при получение жанра(такое название отсутвует)</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getgenrebyname")]
    public async Task<GetGenreByNameDtoResponse> GetGenreByName(string name)
    {
        return await _genreServices.GetGenreByName(name);
    }
    
    /// <summary>
    /// Добавляет жарн для игры(нужно быть админом)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Вы не авторизованы как Admin</response>
    /// <response code="500">Ошибка при создание жанра</response>
    [HttpPost]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("addgenre"), Authorize("Admin")]
    public async Task<bool> AddGenre(AddGenreDto request)
    {
        return await _genreServices.AddGenre(request);
    }
}