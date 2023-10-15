namespace Ecommerce.Domain.Exceptions;

public class ValidacaoException : ExceptionPersonalizada
{
    protected ValidacaoException(string mensagem)
        : base(mensagem)
    {
    }

    public static ValidacaoException Erro(string mensagem) => new ValidacaoException(mensagem);
    
}