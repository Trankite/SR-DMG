using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class Command<TExecute>(Action<TExecute> Execute) : ICommand
    {
        private readonly Action<TExecute> _Execute = Execute;

        public bool CanExecute(object? Parameter) => true;

        public void Execute(object? Parameter)
        {
            if (Parameter is TExecute Parm) _Execute(Parm);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class Command<TExecute, TCanExecute>(Action<TExecute> Execute, Func<TCanExecute, bool> CanExecute) : ICommand
    {
        private readonly Action<TExecute> _Execute = Execute;

        private readonly Func<TCanExecute, bool> _CanExecute = CanExecute;

        public bool CanExecute(object? Parameter) => Parameter is TCanExecute Parm && _CanExecute(Parm);

        public void Execute(object? Parameter)
        {
            if (Parameter is TExecute Parm) _Execute(Parm);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}