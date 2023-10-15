namespace Ecommerce.Domain.Exceptions;

public class RequisicaoInvalidaException : ExceptionPersonalizada
{
    public RequisicaoInvalidaException(string mensagem = ExceptionConstants.RequisicaoInvalida)
        : base(mensagem)
    {
    }

    public static RequisicaoInvalidaException PorMotivo(string motivo) => new(motivo);
    public static RequisicaoInvalidaException CorpoInvalido() => new("Corpo da requisição inválido");
}