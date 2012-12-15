using FWMonyker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWMonyker.Command
{
    public class DeleteAccountCommand : BaseCommand, IUndoRedoCommand
    {
        private IList<Account> Accounts;
        private Account AccountToBeDeleted;

        public DeleteAccountCommand(IList<Account> accounts, Account accountToBeDeleted)
        {
            Accounts = accounts;
            AccountToBeDeleted = accountToBeDeleted;
        }

        public void Execute()
        {
            Accounts.Remove(AccountToBeDeleted);
        }

        public void UnExecute()
        {
            Accounts.Add(AccountToBeDeleted);
        }
    }
}
