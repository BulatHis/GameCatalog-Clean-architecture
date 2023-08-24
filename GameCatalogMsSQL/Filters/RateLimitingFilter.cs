using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameCatalogCore.Filters;

public class RateLimitingFilter : ActionFilterAttribute
{
    private readonly int _limit; // Maximum number of requests
    private readonly TimeSpan _period; // Period within which requests are considered
    private readonly Dictionary<string, List<DateTime>> _requestHistory; // Request history by clients

    public RateLimitingFilter(string limit, string period)
    {
        _limit = int.Parse(limit);
        _period = TimeSpan.Parse(period);
        _requestHistory = new Dictionary<string, List<DateTime>>();
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var clientId = context.HttpContext.Connection.RemoteIpAddress.ToString();
        var currentTime = DateTime.Now;

        // Проверяем, есть ли история запросов от данного клиента
        if (!_requestHistory.ContainsKey(clientId))
        {
            _requestHistory[clientId] = new List<DateTime>();
        }

        // Очищаем историю запросов, удаляя все записи, которые старше указанного периода
        _requestHistory[clientId].RemoveAll(time => currentTime - time > _period);

        // Проверяем количество запросов клиента в указанном периоде
        if (_requestHistory[clientId].Count >= _limit)
        {
            // Если количество запросов превышает лимит, возвращаем ошибку
            context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            return;
        }

        // Добавляем текущий запрос в историю
        _requestHistory[clientId].Add(currentTime);
    }
}
