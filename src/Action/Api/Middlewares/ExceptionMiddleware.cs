using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.Core.Domain.Impl.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Actions.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // must be awaited
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            if (exception is ArgumentNullException) code = HttpStatusCode.BadRequest;
            else if (exception is HttpRequestException) code = HttpStatusCode.BadRequest;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            // Excecao de regra de negocio
            else if (exception is DomainException) code = HttpStatusCode.BadRequest;
            // Erro de item nao encontrado
            else if (exception is ItemNotFoundException) return WriteNotFoundExceptionAsync(context, (ItemNotFoundException)exception);
            // Erro de validacao de entidade
            else if (exception is ValidationException) return WriteValidationExceptionAsync(context, (ValidationException)exception);

            return WriteExceptionAsync(context, exception, code);
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            return response.WriteAsync(JsonSerializer.Serialize(new[]
            {
                exception.Message,
            }));
        }

        private static Task WriteValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            return response.WriteAsync(JsonSerializer.Serialize(exception.Errors.Select(x => x.ErrorMessage).ToArray()));
        }

        private static Task WriteNotFoundExceptionAsync(HttpContext context, ItemNotFoundException ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            return response.WriteAsync(JsonSerializer.Serialize($"The item {ex.Item} you looking for was not found"));
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
