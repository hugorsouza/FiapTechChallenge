namespace Ecommerce.Application.Model.Pessoas.Autenticacao
{
    public record LoginModel
    {
        public string Email { get; init; }
        public string Senha { get; init; }
    }
}
