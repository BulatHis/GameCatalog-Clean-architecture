using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GameCatalogCore.Filters;

public class ExecutionTimeAttribute : ActionFilterAttribute
{
    private Stopwatch _stopwatch = null!;


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        TimeSpan executionTime = _stopwatch.Elapsed;
        if (executionTime.TotalSeconds > 10)
        {
            throw new Exception("Нет подключения к базе данных");
            context.Result = new StatusCodeResult(500); // 500 - внутренняя ошибка сервера
        }
    }
}