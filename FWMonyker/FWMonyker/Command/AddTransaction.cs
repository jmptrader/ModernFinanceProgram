using FWMonyker.Model;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class AddTransaction : BaseCommand, IUndoRedoCommand
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