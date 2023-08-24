using GameCatalogCore.DTO_s.AdminReviewDTO_s;
using GameCatalogCore.Filters;
using GameCatalogCore.IServices;
using GameCatalogDomain.Entity;
using GameCatalogDomain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogAplication.Controllers;

[ApiController]
public class AdminReviewController : ControllerBase
{
    private readonly IAdminReviewServices _adminReviewServices;

    public AdminReviewController(IAdminReviewServices adminReviewServices)
    {
        _adminReviewServices = adminReviewServices;
    }

    
    /// <summary>
    /// Добавить обзор от админа (требуется роль админа)
    /// </summary>
    /// <response code="200">Обзор успешно добавлен</response>
    /// <response code="500">ошибка при добавление обзора</response>
    [HttpPost]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("addadminreview"), Authorize("Admin")]
    public async Task<bool> AddAdminReview(AdminReviewAddDtoRequest request)
    {
        return await _adminReviewServices.Add(request);
    }

    /// <summary>
    /// Берет обзор админов по id игры, нужно при просмотре страницы игры 
    /// </summary>
    /// <param name="request">Данные запроса</param>
    /// <returns>Возвращает true, если добавление обзора прошло успешно</returns>
    /// <response code="200">Обзор найден</response>
    /// <response code="500">Ошибка при поиске(скорее всего его нет)</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("getadminreviewbygameid")]
    public async Task<AdminReviewAddDtoResponse> GetAdminReviewByGameId(Guid gameId)
    {
        return await _adminReviewServices.GetByGameId(gameId);
    }

    /// <summary>
    /// Если нужно будет удалить обзор админа (требуется роль админа)
    /// </summary>
    /// <param name="request">Данные запроса</param>
    /// <returns>Возвращает true, если добавление обзора прошло успешно</returns>
    /// <response code="200">Обзор удален</response>
    /// <response code="500">Ошибка при удаление</response>
    [HttpGet]
    [ExecutionTime]
    [CustomExceptionFilter]
    [Route("deleteadminreview"), Authorize("Admin")]
    public async Task<bool> Delete(Guid adminReviewId)
    {
        return await _adminReviewServices.Delete(adminReviewId);
    }
    
}