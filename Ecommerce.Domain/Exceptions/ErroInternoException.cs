namespace Ecommerce.Domain.Exceptions
{
    public class ErroInternoException : ExceptionPersonalizada
    {
        public ErroInternoException(Exception innerException, string mensagemAmigavel)
            : base(mensagemAmigavel, innerException)
        {

        }

        public static ErroInternoException PorMotivo(Exception innerException, string mensagemAmigavel) => new ErroInternoException(innerException, mensagemAmigavel);
    }
}
