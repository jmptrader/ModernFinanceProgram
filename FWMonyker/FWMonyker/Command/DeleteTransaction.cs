using FWMonyker.Model;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class DeleteTransaction : BaseCommand, IUndoRedoCommand
    {
        private IList<Transaction> Transactions;
        private IList<Transaction> UITransactions;
        private Transaction TransactionToBeDeleted;
        private Account Account;

        public DeleteTransaction(IList<Transaction> accountTransactions, IList<Transaction> uiTransactions, Transaction transaction)
        {
            Transactions = accountTransactions;
            UITransactions = uiTransactions;
            TransactionToBeDeleted = transaction;
            Account = transaction.Account;
        }

        public void Execute()
        {
            Transactions.Remove(TransactionToBeDeleted);
            UITransactions.Remove(TransactionToBeDeleted);
            Account.Balance = Account.Balance - TransactionToBeDeleted.Amount;
        }

        public void UnExecute()
        {
            Transactions.Add(TransactionToBeDeleted);
            UITransactions.Add(TransactionToBeDeleted);
            Account.Balance = Account.Balance + TransactionToBeDeleted.Amount;
        }
    }
}