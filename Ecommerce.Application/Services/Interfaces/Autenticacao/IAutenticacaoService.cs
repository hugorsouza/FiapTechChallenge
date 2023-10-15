using Ecommerce.Application.Model.Autenticacao;

namespace Ecommerce.Application.Services.Interfaces.Autenticacao
{
    public interface IAutenticacaoService
    {
        Task<LoginResponse> Login(LoginModel credenciais);
    }
}
