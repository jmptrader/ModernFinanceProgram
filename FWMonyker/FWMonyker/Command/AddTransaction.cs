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
        private Transaction InitialTransaction;

        public AddTransaction(IList<Transaction> accountTransactions, IList<Transaction> uiTransactions, Transaction initialTransaction, Transaction newTransaction)
        {
            Transactions = accountTransactions;
            UITransactions = uiTransactions;
            NewTransaction = newTransaction;
            InitialTransaction = initialTransaction;
        }

        public void Execute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(InitialTransaction)] = NewTransaction;
                UITransactions[UITransactions.IndexOf(InitialTransaction)] = NewTransaction;
            }
            else
            {
                Transactions.Add(NewTransaction);
                UITransactions.Add(NewTransaction);
            }
        }

        public void UnExecute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(NewTransaction)] = InitialTransaction;
                UITransactions[UITransactions.IndexOf(NewTransaction)] = InitialTransaction;
            }
            else
            {
                Transactions.Remove(NewTransaction);
                UITransactions.Remove(NewTransaction);
            }
        }
    }
}
