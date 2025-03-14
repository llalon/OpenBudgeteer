using OpenBudgeteer.Core.Data.Entities.Models;

namespace OpenBudgeteer.Core.Data.Contracts.Services;

public interface IRecurringBankTransactionService : IBaseService<RecurringBankTransaction>
{
    public RecurringBankTransaction GetWithEntities(Guid id);
    public IEnumerable<RecurringBankTransaction> GetAllWithEntities();
    public Task<IEnumerable<BankTransaction>> GetPendingBankTransactionAsync(DateOnly yearMonth);
    public Task<IEnumerable<BankTransaction>> CreatePendingBankTransactionAsync(DateOnly yearMonth);
}