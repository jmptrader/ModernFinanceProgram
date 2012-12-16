/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using FWMonyker.Model;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class AddAccountCommand : BaseCommand, IUndoRedoCommand
    {
        private IList<Account> Accounts;
        private Account InitialStateAccount;
        private Account EndStateAccount;

        public AddAccountCommand(IList<Account> accounts, Account initialAccount, Account endStateAccount)
        {
            Accounts = accounts;
            InitialStateAccount = initialAccount;
            EndStateAccount = endStateAccount;
        }

        public void Execute()
        {
            if (InitialStateAccount != null)
            {
                Accounts[Accounts.IndexOf(InitialStateAccount)] = EndStateAccount;
            }
            else
            {
                Accounts.Add(EndStateAccount);
            }
        }

        public void UnExecute()
        {
            if (InitialStateAccount != null)
            {
                Accounts[Accounts.IndexOf(EndStateAccount)] = InitialStateAccount;
            }
            else
            {
                Accounts.Remove(EndStateAccount);
            }
        }
    }
}