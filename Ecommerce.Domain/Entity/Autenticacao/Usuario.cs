namespace Ecommerce.Domain.Entity.Autenticacao
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NomeExibicao { get; set; }
        public string Email { get; set; }
        public string EmailNormalizado { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public PerfilUsuario Perfil { get; set; }
    }
}
