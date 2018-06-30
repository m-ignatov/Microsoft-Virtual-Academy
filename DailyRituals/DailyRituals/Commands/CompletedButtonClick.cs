using DailyRituals.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DailyRituals.Commands
{
    class CompletedButtonClick:ICommand
    {
        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            App.DataModel.CompleteRitualToday((Ritual)parameter);
        }
    }
}
