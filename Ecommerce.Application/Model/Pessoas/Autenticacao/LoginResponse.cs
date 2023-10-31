namespace Ecommerce.Application.Model.Pessoas.Autenticacao
{
    public record LoginResponse
    {
        public string AccessToken { get; init; }
        public DateTime ExpiraEmUtc { get; init; }
    }
}
