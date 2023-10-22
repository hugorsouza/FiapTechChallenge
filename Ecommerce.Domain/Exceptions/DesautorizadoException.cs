namespace Ecommerce.Domain.Exceptions;

public class DesautorizadoException : ExceptionPersonalizada
{
    public DesautorizadoException(string mensagem) : base(mensagem)
    {
    }

    public static DesautorizadoException PorMotivo(string mensagem) => new DesautorizadoException(mensagem);
    public static DesautorizadoException RequerPermissaoAdmin() => PorMotivo("A ação solicitada requer permissões de administrador");
}