using Ecommerce.Domain.Entities.Shared;

namespace Ecommerce.Domain.Interfaces.Repository
{
    public interface IRepositoryBase
    {

    }
    public interface IRepositoryBase<TEntity> : IRepositoryBase
        where TEntity : EntityBase
    {
    }
}
