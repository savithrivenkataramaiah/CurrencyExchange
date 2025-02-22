using CurrencyExchange.Core.Entities;

namespace CurrencyExchange.Core.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Transfer CreateTransfer(Transfer transfer);
        Transfer GetTransferByTransferId(Guid transferId);
    }
}
