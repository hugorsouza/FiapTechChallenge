namespace Ecommerce.Domain.Exceptions;

public abstract class ExceptionPersonalizada : Exception
{
    public ExceptionPersonalizada(string mensagem)
        : base(mensagem)
    {
        
    }
}