using Ecommerce.Domain.Entities.Shared;
using Ecommerce.Domain.Interfaces.Repository;
using Ecommerce.Infra.Dados.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infra.Dados.Repositories.Shared
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        protected ApplicationDbContext Context { get; }

        protected RepositoryBase(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            Context = context;
        }
    }

    public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected RepositoryBase(ApplicationDbContext context) : base(context)
        {
        }

        private DbSet<TEntity> _dbSet;

        private DbSet<TEntity> CreateDbSet()
        {
            return Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet
        {
            get { return _dbSet ??= CreateDbSet(); }
        }
    }
}
