using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieShop.Core.Exceptions;
using Newtonsoft.Json;

namespace MovieShop.API.Infrastructure
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //handle when any exception happens
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                //handle exception
                await HandleExceptionAsync(httpContext, ex);
            }
            
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //get and return exception information in friendly json format to angular
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            string result;
            //object errors = null;

            if (env.IsDevelopment())
            {

            }

            //error object
            var errorDetails = new ErrorResponseModel
            {
                ErrorMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                InnerException = ex.InnerException.Message
            };

            switch (ex)
            {
                case BadRequestException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //errors = ex.Message;
                    break;
                case ConflictException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    //errors = ex.Message;
                    break;
                case NotFoundException _:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    //errors = ex.Message;
                    break;
                case HttpException re:
                    //errors = re.Errors;
                    httpContext.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    //errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : "Server error, please try later";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                //case UnauthorizedAccessException _:
                //    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //    break;
                //case Exception _:
                //    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //    break;
                default:
                    break;
            }

            result = JsonConvert.SerializeObject(new { errors = errorDetails });
            _logger.LogError(result);
            // install Serilog or NLog 3rd party libraries for logging to text file, db
            await httpContext.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
