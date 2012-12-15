namespace FWMonyker.Command
{
    public interface IUndoRedoCommand
    {
        void Execute();

        void UnExecute();
    }
}