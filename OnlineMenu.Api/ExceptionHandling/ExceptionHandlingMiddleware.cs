using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineMenu.Domain.Exceptions;
using static Newtonsoft.Json.JsonConvert;

namespace OnlineMenu.Api.ExceptionHandling
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly bool isDevelopment;

        public ExceptionHandlingMiddleware(RequestDelegate next, bool isDevelopment)
        {
            this.next = next;
            this.isDevelopment = isDevelopment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            } // TODO: When you add a custom exception, you can add a handler here
            catch (ArgumentException argumentException)
            {
                await HandleBadValueExceptionAsync(context, argumentException);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private static async Task HandleBadValueExceptionAsync(HttpContext context, ArgumentException badValueException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(SerializeObject(new ErrorDto(badValueException.Message)));
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            if (isDevelopment)
                await context.Response.WriteAsync(SerializeObject(new ErrorDto(exception.Message)));
            else
                await context.Response.WriteAsync("Something went wrong, please try again in a few minutes");
        }
    }
}
