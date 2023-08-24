using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameCatalogCore.Filters;

public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Handle different types of exceptions
        if (exception is UnauthorizedAccessException)
        {
            // Unauthorized exception handling
            var errorResponse = new ErrorResponse
            {
                Code = 401,
                Message = "Unauthorized."
            };

            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.Unauthorized
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
        else if (exception is NotFound)
        {
            // Not found exception handling
            var errorResponse = new ErrorResponse
            {
                Code = 404,
                Message = "Not found."
            };

            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
        else
        {
            // Default error handling
            var errorResponse = new ErrorResponse
            {
                Code = 500,
                Message = "An error occurred."
            };

            var result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}


public class ErrorResponse
{
    public int Code { get; set; } 
    public string Message { get; set; } = null!;
}