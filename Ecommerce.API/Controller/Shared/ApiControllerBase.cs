using System.Net;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controller.Shared;

public abstract class ApiControllerBase : ControllerBase
{
    [NonAction]
    protected IActionResult TratarErro(Exception ex)
    {
        var (statusCode, titulo) = ex switch
        {
            ValidacaoException => (HttpStatusCode.BadRequest, "Ocorreram um ou mais erros de validação"),
            RequisicaoInvalidaException => (HttpStatusCode.BadRequest , "Requisição inválida"),
            _ => (HttpStatusCode.InternalServerError, "Erro interno do servidor")
        };
        
        return StatusCode((int)statusCode, new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7807",
            Title = titulo,
            Status = (int)statusCode,
            Instance = Request.Path,
            Detail = ex is RequisicaoInvalidaException badReq 
                    ? badReq.Message 
                    : ex.ToString()
        });
    }
}