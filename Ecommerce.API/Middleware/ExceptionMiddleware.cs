using Ecommerce.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (RequisicaoInvalidaException badReqEx)
            {
                EscreverRetornoBadRequest(badReqEx, context);
            }
            catch (ValidationException valEx)
            {
                EscreverRetornoErroValidacao(valEx, context);
            }
            catch (Exception ex)
            {
                EscreverRetornoErroPadrao(ex, context);
            }
        }

        private void EscreverRetornoErroValidacao(ValidationException ex, HttpContext context)
        {
            const int statusCode = StatusCodes.Status400BadRequest;
            var listaErros = ex.Errors
                .GroupBy(err => err.PropertyName)
                .ToDictionary(err => err.Key, 
                    err => err.Select(x => x.ErrorMessage
                    ).ToArray());

            var problemDetails = new ValidationProblemDetails(listaErros)
            {
                Title = "Requisição inválida",
                Detail = listaErros.Count == 1 
                    ? "Ocorreu um erro de validação"
                    : "Ocorreram múltiplos erros de validação",
                Status = statusCode,
                Instance = context.Request.Path,
                Type = ""
            };
            EscreverResponse(context, statusCode, problemDetails);
        }

        private void EscreverRetornoBadRequest(Exception ex, HttpContext context)
        {
            const int statusCode = StatusCodes.Status400BadRequest;
            var problemDetails = new ProblemDetails()
            {
                Title = "Requisição inválida",
                Detail = ex.Message,
                Status = statusCode,
                Instance = context.Request.Path,
                Type = ""
            };

            EscreverResponse(context, statusCode, problemDetails);
        }

        private void EscreverRetornoErroPadrao(Exception ex, HttpContext context)
        {
            const int statusCode =  StatusCodes.Status500InternalServerError;
            var esconderDetalhes = EsconderDetalhesErro();
            var problemDetails = new ProblemDetails()
            {
                Title = "Erro interno",
                Detail = esconderDetalhes ? "Ocorreu um erro inesperado" : ex.Message,
                Status = statusCode,
                Instance = context.Request.Path,
                Type = esconderDetalhes ? "" : ex.HelpLink
            };

            EscreverResponse(context, statusCode, problemDetails);
        }
        
        private static void EscreverResponse(HttpContext context, int statusCode, object retorno, string content = "application/json")
        {
            context.Response.ContentType = content;
            context.Response.StatusCode = statusCode;
            context.Response.WriteAsJsonAsync(retorno);
        }

        private bool EsconderDetalhesErro() => _environment.IsProduction();
    }
}
