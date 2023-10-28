namespace Ecommerce.Application.Model.Pessoas.Autenticacao
{
    public record LoginWithRefreshResponse
    {
        public string AccessToken { get; init; }
        public DateTime ExpiraEmUtc { get; init; }
        public string RefreshToken { get; init; }
    }
}
