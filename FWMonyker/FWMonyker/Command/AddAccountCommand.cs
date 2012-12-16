using FWMonyker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWMonyker.Command
{
    public class AddAccountCommand : BaseCommand, IUndoRedoCommand
    {
        private IList<Account> Accounts;
        private Account InitialStateAccount;
        private Account EndStateAccount;

        public AddAccountCommand(IList<Account> accounts, Account initialAccount, Account endStateAccount )
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
