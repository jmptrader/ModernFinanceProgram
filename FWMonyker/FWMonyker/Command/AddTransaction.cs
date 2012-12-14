using FWMonyker.Model;
using FWMonyker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    class AddTransaction : BaseCommand, IUndoRedoCommand
    {
        private IList<Transaction> Transactions;
        private IList<Transaction> UITransactions;
        private Transaction NewTransaction;

        public AddTransaction(IList<Transaction> accountTransactions, IList<Transaction> uiTransactions, Transaction newTransaction)
        {
            Transactions = accountTransactions;
            UITransactions = uiTransactions;
            NewTransaction = newTransaction;
        }

        public void Execute()
        {
            Transactions.Add(NewTransaction);
            UITransactions.Add(NewTransaction);
        }


        public void UnExecute()
        {
            Transactions.Remove(NewTransaction);
            UITransactions.Remove(NewTransaction);
        }
    }
}
