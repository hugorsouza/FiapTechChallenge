namespace Ecommerce.Application.Model.Pessoas.Autenticacao
{
    public record LoginResponse : IRespostaBase
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
        public DateTime ExpiraEmUtc { get; init; }
        public string Mensagem { get; init; }
    }
}
