namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IRepository
    {
        
    }
    public interface IRepository<T> : IRepository where T : class
    {
        public void Cadastrar(T entidade);
        public T ObterPorId(int id);
        public IList<T> ObterTodos();
        public void Alterar(T entidade);
        public void Deletar(int id);
    }
}
