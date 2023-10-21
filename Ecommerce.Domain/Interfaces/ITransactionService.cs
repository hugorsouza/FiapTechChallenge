namespace Ecommerce.Domain.Interfaces;

/// <summary>
/// Implementação simples de uma unit of work usada para coodenar transações fora da camada de infra.
/// </summary>
public interface ITransactionService : IDisposable
{
    void BeginTransaction();
    void Commit();
    void Rollback();
    bool TryRollback();
}