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
        private Account Account;

        public AddTransaction(IList<Transaction> accountTransactions, IList<Transaction> uiTransactions, Transaction initialTransaction, Transaction newTransaction)
        {
            Transactions = accountTransactions;
            UITransactions = uiTransactions;
            NewTransaction = newTransaction;
            InitialTransaction = initialTransaction;
            Account = NewTransaction.Account;
        }

        public void Execute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(InitialTransaction)] = NewTransaction;
                UITransactions[UITransactions.IndexOf(InitialTransaction)] = NewTransaction;
                Account.Balance = Account.Balance + (NewTransaction.Amount - InitialTransaction.Amount);
            }
            else
            {
                Transactions.Add(NewTransaction);
                UITransactions.Add(NewTransaction);
                Account.Balance = Account.Balance + NewTransaction.Amount;
            }
        }

        public void UnExecute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(NewTransaction)] = InitialTransaction;
                UITransactions[UITransactions.IndexOf(NewTransaction)] = InitialTransaction;
                Account.Balance = Account.Balance + (InitialTransaction.Amount - NewTransaction.Amount);
            }
            else
            {
                Transactions.Remove(NewTransaction);
                UITransactions.Remove(NewTransaction);
                Account.Balance = Account.Balance - NewTransaction.Amount;
            }
        }
    }
}