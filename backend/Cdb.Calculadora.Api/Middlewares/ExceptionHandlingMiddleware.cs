using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Cdb.Calculadora.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context); // segue o pipeline
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors.Select(e => new
                {
                    Campo = e.PropertyName,
                    Erro = e.ErrorMessage
                });

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 400,
                    erros = errors
                }));

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    status = 500,
                    erro = "Erro interno no servidor.",
                    detalhes = ex.Message
                }));
            }
        }
    }

}
