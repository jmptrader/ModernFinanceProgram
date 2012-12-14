using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FWMonyker.Command
{
    public interface IUndoRedoCommand
    {
        void Execute();
        void UnExecute();
    }
}
