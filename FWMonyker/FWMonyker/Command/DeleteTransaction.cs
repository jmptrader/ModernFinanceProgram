using FWMonyker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWMonyker.Command
{
    class DeleteTransaction : BaseCommand, IUndoRedoCommand
    {
        private IList<Transaction> Transactions;
        private IList<Transaction> UITransactions;
        private Transaction TransactionToBeDeleted;

        public DeleteTransaction(IList<Transaction> accountTransactions, IList<Transaction> uiTransactions, Transaction transaction)
        {
            Transactions = accountTransactions;
            UITransactions = uiTransactions;
            TransactionToBeDeleted = transaction;
        }

        public void Execute()
        {
            Transactions.Remove(TransactionToBeDeleted);
            UITransactions.Remove(TransactionToBeDeleted);
        }


        public void UnExecute()
        {
            Transactions.Add(TransactionToBeDeleted);
            UITransactions.Add(TransactionToBeDeleted);
        }
    }
}
