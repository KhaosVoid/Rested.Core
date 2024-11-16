using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rested.Core.MediatR.Validation;
using System.Net;

namespace Rested.Core.Server.Validation;

public class RestedValidationExceptionMiddleware : IMiddleware
{
    #region Members

    private readonly ILogger<RestedValidationExceptionMiddleware> _logger;

    #endregion Members

    #region Ctor

    public RestedValidationExceptionMiddleware(ILogger<RestedValidationExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    #endregion Ctor

    #region Methods

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException, validationException.Message);

            await HandleExceptionAsync(httpContext, validationException);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException validationException)
    {
        var response = new
        {
            Detail = validationException.Message,
            Errors = GetErrors(validationException)
        };

        httpContext.Response.StatusCode = GetHttpStatusCodeFromValidationException(validationException);

        await httpContext.Response.WriteAsJsonAsync(response);
    }

    private static int GetHttpStatusCodeFromValidationException(ValidationException validationException)
    {
        if (validationException.Errors.Count() is 1)
            return int.Parse(validationException.Errors.First().ErrorCode.Split('.').First());

        return (int)HttpStatusCode.BadRequest;
    }

    private static IEnumerable<ValidationError> GetErrors(ValidationException validationException)
    {
        return validationException.Errors.Select(ValidationError.FromValidationFailure);
    }

    #endregion Methods
}