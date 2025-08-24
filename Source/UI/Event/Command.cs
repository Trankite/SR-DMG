using System.Windows.Input;

namespace SR_DMG.Source.UI.Event
{
    public class Command<T>(Action<T> Execute, Func<T, bool>? CanExecute = null) : ICommand
    {
        private readonly Action<T> _Execute = Execute;
        private readonly Func<T, bool>? _CanExecute = CanExecute;

        public bool CanExecute(object? Parameter) => _CanExecute == null || _CanExecute((T)(Parameter ?? false));

        public void Execute(object? Parameter) => _Execute((T)(Parameter ?? false));

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}