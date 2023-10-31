namespace Ecommerce.Domain.Exceptions;

public abstract class ExceptionPersonalizada : Exception
{
    public ExceptionPersonalizada(string mensagem, Exception innerException = null)
        : base(mensagem, innerException)
    {
        
    }
}