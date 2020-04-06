using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ConvertersDemo.ViewModels
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private int _index;
        private int _max = 6;

        public Command Next { get; set; }
        public Command Previous { get; set; }
        public MainPageViewModel()
        {
            Index = 0;
            Next = new Command(
                () => { Index += 1; },
                () => (Index < _max - 1)
            );
            Previous = new Command(
                () => { Index -= 1; },
                () => (Index > 0)
            );
        }

        public int Index { 
            get { return _index; } 
            set { 
                _index = value; 
                if (Next != null) ((Command)Next).ChangeCanExecute();
                if (Previous != null) ((Command)Previous).ChangeCanExecute();
                NotifyPropertyChanged();
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
