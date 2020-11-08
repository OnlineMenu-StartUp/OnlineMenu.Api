using System;
using System.IO;
using System.Net;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Newtonsoft.Json.JsonConvert;
using OnlineMenu.Domain.Exceptions;

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
            catch (AuthenticationException authenticationException)
            {
                await AuthenticationExceptionHandler(context, authenticationException);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private async Task AuthenticationExceptionHandler(HttpContext context, AuthenticationException authenticationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var message = string.IsNullOrEmpty(authenticationException.Message)
                ? "Incorrect user name or password"
                : authenticationException.Message;
            await context.Response.WriteAsync(SerializeObject(new ErrorDto(message)));
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
                await context.Response.WriteAsync(exception.ToString());
            else
                await context.Response.WriteAsync("Something went wrong, please try again in a few minutes");
        }
    }
}
