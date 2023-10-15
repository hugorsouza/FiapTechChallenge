namespace Ecommerce.Application.Model.Autenticacao
{
    public record LoginModel
    {
        public string Email { get; init; }
        public string Senha { get; init; }
    }
}
