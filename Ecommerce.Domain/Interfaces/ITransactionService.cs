namespace Ecommerce.Domain.Interfaces;

/// <summary>
/// Implementação simples de uma unit of work usada para coodenar transações fora da camada de infra.
/// </summary>
public interface ITransactionService : IDisposable
{
    public void BeginTransaction();
    public void Commit();
    public void Rollback();
    public bool TryRollback();
}