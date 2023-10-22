using Ecommerce.Application.Model.Pessoas.Autenticacao;

namespace Ecommerce.Application.Services.Interfaces.Autenticacao
{
    public interface IAutenticacaoService
    {
        Task<LoginResponse> Login(LoginModel credenciais);
    }
}
