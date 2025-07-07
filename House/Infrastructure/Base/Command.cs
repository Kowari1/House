using System.Windows.Input;

namespace House.Infrastructure.Base
{
    /// <summary>
    /// Базовая команда.
    /// </summary>
    public abstract class Command : ICommand
    {

        /// <summary>
        /// Событие, вызываемое при изменении условий выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Проверяет, может ли команда быть выполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если выполнение разрешено; иначе False.</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public abstract void Execute(object parameter);
    }
}
