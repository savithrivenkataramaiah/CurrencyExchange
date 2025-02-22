using CurrencyExchange.Core.Entities;
using CurrencyExchange.Core.Interfaces.Repositories;

namespace CurrencyExchange.Infrastructure
{
    public class TransferRepository : ITransferRepository
    {
        private readonly List<Transfer> _transfers = new List<Transfer>();
        public Transfer CreateTransfer(Transfer transfer)
        {
            _transfers.Add(transfer);
            return transfer;
        }

        public Transfer GetTransferByTransferId(Guid transferId)
        {
            return _transfers.FirstOrDefault(transfer => transfer.TransferId == transferId);
        }
    }
}
