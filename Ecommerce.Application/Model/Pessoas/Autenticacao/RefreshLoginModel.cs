namespace Ecommerce.Application.Model.Pessoas.Autenticacao
{
    public record RefreshLoginModel
    {
        public string Email { get; init; }
        public string RefreshToken { get; init; }
    }
}
