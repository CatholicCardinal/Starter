     using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Commands.Base;

namespace Starter.Commands
{
    internal class RelayCommand : Command
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public RelayCommand(Action<Object> Execute, Func<object, bool> CanExecute = null)
        {
            execute = Execute ?? throw new ArgumentException(nameof(Execute));
            canExecute = CanExecute;
        }

        public override bool CanExecute(object? parameter) => canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => execute(parameter);
        
    }
}
