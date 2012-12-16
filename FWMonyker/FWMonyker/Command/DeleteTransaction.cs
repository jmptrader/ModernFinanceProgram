using FWMonyker.Model;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class DeleteTransaction : BaseCommand, IUndoRedoCommand
    {
        private IList<Transaction> Transactions;
        private Transaction TransactionToBeDeleted;
        private Account Account;

        public DeleteTransaction(IList<Transaction> accountTransactions, Transaction transaction)
        {
            Transactions = accountTransactions;
            TransactionToBeDeleted = transaction;
            Account = transaction.Account;
        }

        public void Execute()
        {
            Transactions.Remove(TransactionToBeDeleted);
            Account.Balance = Account.Balance - TransactionToBeDeleted.Amount;
        }

        public void UnExecute()
        {
            Transactions.Add(TransactionToBeDeleted);
            Account.Balance = Account.Balance + TransactionToBeDeleted.Amount;
        }
    }
}