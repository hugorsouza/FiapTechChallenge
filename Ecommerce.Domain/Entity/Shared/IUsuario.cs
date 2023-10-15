using Ecommerce.Domain.Entity.Autenticacao;

namespace Ecommerce.Domain.Entity.Shared
{
    public interface IUsuario
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; }
    }
}
