﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TourfirmApplication.Support
{
    public class MyCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public MyCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        //public bool CanExecute(object parameter) // эквивалентно вышезаписанному коду
        //{
        //    return _canExecute == null || _canExecute(parameter);
        //}

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
