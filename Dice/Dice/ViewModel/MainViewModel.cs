using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Dice.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _number;
        private int _max;
        private Random _random;

        public Command Roll { get; set; }
        public Command<string> SetMax { get; set; }

        public MainViewModel()
        {
            _random = new Random();
            Max = 6;
            Number = _random.Next(Max);
            Roll = new Command(
                () => { Number = _random.Next(Max); }
            );
            SetMax = new Command<string>(
                (parameter) => 
                {
                    if (Int32.TryParse(parameter as string, out int result))
                    {
                        Max = result;
                    }
                },
                (parameter) => 
                {
                    if (Int32.TryParse(parameter as string, out int result))
                    {
                        if (result != Max) return true;
                    }
                    return false;
                }
            );
        }

        public int Number { get { return _number + 1; } set { _number = value; NotifyPropertyChanged(); } }
        public int Max { get { return _max; } set { 
                _max = value; 
                if (Roll != null && Roll.CanExecute(null)) Roll.Execute(null);
                if (SetMax != null)((Command)SetMax).ChangeCanExecute();
                NotifyPropertyChanged(); 
            } }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RefreshCanExecutes()
        {
            ((Command)SetMax).ChangeCanExecute();
        }
    }
}
