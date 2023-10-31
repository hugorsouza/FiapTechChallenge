using Ecommerce.Application.Services.Interfaces.Autenticacao;
using Ecommerce.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IUsuarioManager _usuarioManager;

        public ExceptionMiddleware(IWebHostEnvironment environment, ILogger<ExceptionMiddleware> logger, IUsuarioManager usuarioManager)
        {
            _environment = environment;
            _logger = logger;
            _usuarioManager = usuarioManager;
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
            catch (DesautorizadoException unauthEx)
            {
                EscreverRetornoUnauthorized(unauthEx, context);
            }
            catch (ValidationException valEx)
            {
                EscreverRetornoErroValidacao(valEx, context);
            }
            catch (ErroInternoException erroInterno)
            {
                EscreverRetornoErroInternoTratado(erroInterno, context);
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
            var rota = ObterRota(context);

            var problemDetails = new ValidationProblemDetails(listaErros)
            {
                Title = "Requisição inválida",
                Detail = listaErros.Count == 1 
                    ? "Ocorreu um erro de validação"
                    : "Ocorreram múltiplos erros de validação",
                Status = statusCode,
                Instance = rota,
                Type = ""
            };
            var mensagemErroLog = string.Join("|", listaErros.Select(x => $"{x.Key}: [\"{(string.Join("\", \"", x.Value))}\"]").ToArray());
            _logger.LogWarning(ex, "{usuario} em {path} - Validação - {mensagem} - {tipo}",
                ObterIdentificadorUsuario(), rota, mensagemErroLog, ex.GetType().FullName);
            EscreverResponse(context, statusCode, problemDetails);
        }

        private void EscreverRetornoBadRequest(Exception ex, HttpContext context)
        {
            const int statusCode = StatusCodes.Status400BadRequest;
            var rota = ObterRota(context);

            var problemDetails = new ProblemDetails()
            {
                Title = "Requisição inválida",
                Detail = ex.Message,
                Status = statusCode,
                Instance = rota,
                Type = ""
            };
            _logger.LogWarning(ex, "{usuario} em {path} - {mensagem} - {stackTrace} - {tipo}",
                ObterIdentificadorUsuario(), rota, ex.Message, ex.StackTrace, ex.GetType().FullName);
            EscreverResponse(context, statusCode, problemDetails);
        }
        
        private void EscreverRetornoUnauthorized(DesautorizadoException ex, HttpContext context)
        {
            const int statusCode = StatusCodes.Status403Forbidden;
            var rota = ObterRota(context);

            var problemDetails = new ProblemDetails()
            {
                Title = "Desautorizado",
                Detail = ex.Message,
                Status = statusCode,
                Instance = rota,
                Type = ""
            };
            _logger.LogWarning(ex, "{usuario} em {path} - {mensagem} - {tipo}",
                ObterIdentificadorUsuario(), rota, ex.Message, ex.GetType().FullName);
            EscreverResponse(context, statusCode, problemDetails);
        }

        private void EscreverRetornoErroInternoTratado(ErroInternoException erroInterno, HttpContext context)
        {
            const int statusCode = StatusCodes.Status500InternalServerError;
            var ex = erroInterno.InnerException;
            var esconderDetalhes = EsconderDetalhesErro();
            var rota = ObterRota(context);

            var problemDetails = new ProblemDetails()
            {
                Title = "Erro interno",
                Detail = erroInterno.Message,
                Status = statusCode,
                Instance = context.Request.Path,
                Type = esconderDetalhes ? "" : ex.HelpLink
            };
            _logger.LogError(ex, "{usuario} em {path} - {mensagem} - {stackTrace} - {tipo}",
                ObterIdentificadorUsuario(), rota, ex.Message, ex.StackTrace, ex.GetType().FullName);
            EscreverResponse(context, statusCode, problemDetails);
        }

        private void EscreverRetornoErroPadrao(Exception ex, HttpContext context)
        {
            const int statusCode =  StatusCodes.Status500InternalServerError;
            var esconderDetalhes = EsconderDetalhesErro();
            var rota = ObterRota(context);

            var problemDetails = new ProblemDetails()
            {
                Title = "Erro interno",
                Detail = esconderDetalhes ? "Ocorreu um erro inesperado" : ex.Message,
                Status = statusCode,
                Instance = context.Request.Path,
                Type = esconderDetalhes ? "" : ex.HelpLink
            };
            _logger.LogError(ex, "{usuario} em {path} - {mensagem} - {stackTrace} - {tipo}", 
                ObterIdentificadorUsuario(), rota, ex.Message, ex.StackTrace, ex.GetType().FullName);
            EscreverResponse(context, statusCode, problemDetails);
        }
        
        private static void EscreverResponse(HttpContext context, int statusCode, object retorno, string content = "application/json")
        {
            context.Response.ContentType = content;
            context.Response.StatusCode = statusCode;
            context.Response.WriteAsJsonAsync(retorno);
        }

        private bool EsconderDetalhesErro() => _environment.IsProduction();

        private string ObterIdentificadorUsuario() => _usuarioManager.ObterEmailUsuarioAtual()?.ToLowerInvariant() ?? "Usuário não logado";

        private string ObterRota(HttpContext context)
        {
            return $"{context.Request.Method} {context.Request.Path}";
        }
    }
}
