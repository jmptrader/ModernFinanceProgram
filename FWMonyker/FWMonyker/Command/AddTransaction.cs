/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *   & Lennart Jacobsen                 *
 *                                      *
 ****************************************
 */

using FWMonyker.Model;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class AddTransaction : BaseCommand, IUndoRedoCommand
    {
        private IList<Transaction> Transactions;
        private Transaction NewTransaction;
        private Transaction InitialTransaction;
        private Account Account;

        public AddTransaction(IList<Transaction> accountTransactions, Transaction initialTransaction, Transaction newTransaction)
        {
            Transactions = accountTransactions;
            NewTransaction = newTransaction;
            InitialTransaction = initialTransaction;
            Account = NewTransaction.Account;
        }

        public void Execute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(InitialTransaction)] = NewTransaction;
                Account.Balance = Account.Balance + (NewTransaction.Amount - InitialTransaction.Amount);
            }
            else
            {
                Transactions.Add(NewTransaction);
                Account.Balance = Account.Balance + NewTransaction.Amount;
            }
        }

        public void UnExecute()
        {
            if (InitialTransaction != null)
            {
                Transactions[Transactions.IndexOf(NewTransaction)] = InitialTransaction;
                Account.Balance = Account.Balance + (InitialTransaction.Amount - NewTransaction.Amount);
            }
            else
            {
                Transactions.Remove(NewTransaction);
                Account.Balance = Account.Balance - NewTransaction.Amount;
            }
        }
    }
}