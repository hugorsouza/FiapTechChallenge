namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IRepository
    {
        
    }
    public interface IRepository<T> : IRepository where T : class
    {
        void Cadastrar(T entidade);
        T ObterPorId(int id);
        IList<T> ObterTodos();
        void Alterar(T entidade);
        void Deletar(int id);
    }
}
