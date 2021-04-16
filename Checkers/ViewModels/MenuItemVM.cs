using Checkers.Commands;
using Checkers.Models;
using Checkers.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModels
{
    class MenuItemVM : BaseNotification
    {
        private MenuItemLogic command;

        public MenuItemVM()
        {
            command = new MenuItemLogic(this);
        }

        private bool canExecuteCommand = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }

            set
            {
                if (canExecuteCommand == value)
                {
                    return;
                }
                canExecuteCommand = value;
            }
        }

        private ICommand newCommand;
        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand<object>(command.New, param => CanExecuteCommand);
                }
                return newCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand<object>(command.Save, param => CanExecuteCommand);
                }
                return saveCommand;
            }
        }

        private ICommand loadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand<object>(command.Load, param => CanExecuteCommand);
                }
                return loadCommand;
            }
        }

        private ICommand settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (settingsCommand == null)
                {
                    settingsCommand = new RelayCommand<object>(command.Settings, param => CanExecuteCommand);
                }
                return settingsCommand;
            }
        }

        private ICommand aboutCommand;
        public ICommand AboutCommand
        {
            get
            {
                if (aboutCommand == null)
                {
                    aboutCommand = new RelayCommand<object>(command.About, param => CanExecuteCommand);
                }
                return aboutCommand;
            }
        }
    }
}
