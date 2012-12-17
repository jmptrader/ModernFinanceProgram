/*
 ****************************************
 *                                      *
 *   Author: Kristian Dam-Jensen        *
 *                                      *
 ****************************************
 */

using FWMonyker.Model;
using FWMonyker.ViewModel;
using System.Collections.Generic;

namespace FWMonyker.Command
{
    public class AddAccountCommand : BaseCommand, IUndoRedoCommand
    {
        private IList<Account> Accounts;
        private Account InitialStateAccount;
        private Account EndStateAccount;
        private MainViewModel Model;

        public AddAccountCommand(IList<Account> accounts, Account initialAccount, Account endStateAccount, MainViewModel model)
        {
            Accounts = accounts;
            InitialStateAccount = initialAccount;
            EndStateAccount = endStateAccount;
            Model = model;
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
            Model.NotifyAccountsChanged();
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
            Model.NotifyAccountsChanged();
        }
    }
}