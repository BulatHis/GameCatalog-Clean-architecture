using System.Globalization;
using GameCatalogCore.DTO_s.ReviewsDTO_s;
using GameCatalogCore.Filters;
using GameCatalogCore.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameCatalogAplication.Controllers;

[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewServices _reviewServices;

    public ReviewController(IReviewServices reviewServices)
    {
        _reviewServices = reviewServices;
    }


    /// <summary>
    ///  Добавить обзор польз-ля (нужно быть авторизованым и с подтвержденной почтой)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="401">Вы не авторизованы</response>
    /// <response code="500">Ошибка при добавление обзора(возможно аккаунт не подтвержден)</response>
    [HttpPost]
    [ExecutionTime]
    [RateLimitingFilter(limit: "2", period: "00:01:30")]
    [CustomExceptionFilter]
    [Route("addreview")] [Authorize(Policy = "User")]
    public async Task<CreateReviewDtoResponse> AddReview(CreateReviewDtoRequest request)
    {
        return await _reviewServices.AddReview(request);
    }

    /// <summary>
    ///  Исправить обзор(не используется пока что на сайте)
    /// </summary>
    /// <response code="200">Успех</response>
    /// /// <response code="401">Вы не авторизованы</response>
    /// <response code="500">Ошибка при обновление обзора</response>
    [HttpPost]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("updatereview"), Authorize("User"), Authorize("Admin")]
    public async Task<UpdateTextReviewDtoResponse> UpdateReviewText(UpdateTextReviewDtoRequest request)
    {
        return await _reviewServices.UpdateReviewText(request);
    }


    /// <summary>
    /// Ищем все обзоры по id игры(используется на странице игры, чтобы посмотреть все обзоры поль-лей)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при получение обзоров (мб их нет)</response>
    [HttpGet]
    [CustomExceptionFilter]
    [ExecutionTime]
    [Route("getreviewbygameid")]
    public async Task<GetReviewByGameIdDtoResponse> GetReviewByGameId([FromQuery] GetReviewByGameIdDtoRequest request)
    {
        return await _reviewServices.GetReviewByGameId(request);
    }

    /// <summary>
    /// Поиск обзора по id(пока не используется)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при поиске обзора(скорее всего - его нет)</response>
    [HttpGet]
    [CustomExceptionFilter]
    [ExecutionTime]
    [Route("getreviewbyid")]
    public async Task<GetReviewByIdDtoResponse> GetReviewById([FromQuery] GetReviewByIdDtoRequest request)
    {
        return await _reviewServices.GetReviewById(request);
    }

    /// <summary>
    /// Удалить обзор (пока не используется на сайте)
    /// </summary>
    /// <response code="200">Успех</response>
    /// <response code="500">Ошибка при удаление обзора(скорее всего такого id нет)</response>
    [HttpPost]
    [CustomExceptionFilter]
    [ExecutionTime]
    [Route("deletereview")]
    public async Task<DeleteReviewDtoResponse> DeleteReview(DeleteReviewDtoRequest request)
    {
        return await _reviewServices.DeleteReview(request);
    }
}