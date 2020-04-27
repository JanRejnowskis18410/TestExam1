using Microsoft.AspNetCore.Http;
using MySolution.Exceptions;
using MySolution.Models;
using System;
using System.Threading.Tasks;

namespace MySolution.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch(Exception exc)
            {
                await HandleExceptionAsync(context, exc);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exc)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if(exc is AnimalsArgumentsException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            if(exc is AnimalsSortParamException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            if(exc is SqlServerException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = "Problem w czasie operacji na bazie danych: " + exc.Message
                }.ToString());
            }

            if(exc is CreateAnimalWrongProcedureIdException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            if(exc is CreateAnimalWrongOwnerIdException)
            {
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)StatusCodes.Status400BadRequest,
                    Message = exc.Message
                }.ToString());
            }

            return context.Response.WriteAsync(new ErrorDetails {
                StatusCode = (int)StatusCodes.Status500InternalServerError,
                Message = "Wystąpił jakiś błąd..."
            }.ToString());
        }
    }
}
