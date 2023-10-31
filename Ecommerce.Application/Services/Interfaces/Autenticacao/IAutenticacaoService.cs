using Ecommerce.Application.Model.Pessoas.Autenticacao;

namespace Ecommerce.Application.Services.Interfaces.Autenticacao
{
    public interface IAutenticacaoService
    {
        Task<LoginWithRefreshResponse> Login(LoginModel credenciais);
        Task<LoginResponse> RefreshLogin(RefreshLoginModel credenciais);
    }
}
