using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); //hata yoksa
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e); //hata varsa
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json"; //tarayıcıya json yollandı
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //hata kodu InternalServerError

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors; //hataları liste yap
            if (e.GetType() == typeof(ValidationException)) //eğer ValidationException varsa hata kodu bunu ver 
            {
                message = e.Message;
                errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails //validation'a uygun hata listesi
                {
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails //sistem hata verirse burayı döndür
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
