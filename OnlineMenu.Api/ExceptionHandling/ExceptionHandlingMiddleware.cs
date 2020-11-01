using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static Newtonsoft.Json.JsonConvert;
using FluentValidation;
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
            catch (BadValueException argumentException)
            {
                await HandleBadValueExceptionAsync(context, argumentException);
            }
            catch (AuthenticationException authenticationException)
            {
                await HandleAuthenticationException(context, authenticationException);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private static async Task HandleBadValueExceptionAsync(HttpContext context, BadValueException badValueException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(SerializeObject(new ErrorDto(badValueException.Message)));
        }

        private async Task HandleAuthenticationException(HttpContext context, AuthenticationException authenticationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var message = string.IsNullOrEmpty(authenticationException.Message)
                ? "Incorrect user name or password"
                : authenticationException.Message;
            await context.Response.WriteAsync(SerializeObject(new ErrorDto(message)));
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
