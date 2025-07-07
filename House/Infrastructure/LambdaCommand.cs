using House.Infrastructure.Base;

namespace House.Infrastructure
{
    /// <summary>
    /// Команда, для реализации поведения Execute и CanExecute без создания отдельного класса.
    /// </summary>
    public class LambdaCommand : Command
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecut;

        /// <summary>
        /// Конструктор команды.
        /// </summary>
        /// <param name="Execute">Делегат, выполняемый при вызове команды.</param>
        /// <param name="CanExecute">Делегат, определяющий, может ли команда выполниться. Необязательный.</param>
        /// <exception cref="ArgumentException">Если <paramref name="Execute"/> равен null.</exception>
        public LambdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentException(nameof(Execute));
            canExecut = CanExecute;
        }

        /// <summary>
        /// Проверяет, может ли команда быть выполнена.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если выполнение разрешено; иначе False.</returns>
        public override bool CanExecute(object parameter) => canExecut?.Invoke(parameter) ?? true;

        /// <summary>
        /// Выполняет команду.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;
            execute(parameter);
        }
    }
}
