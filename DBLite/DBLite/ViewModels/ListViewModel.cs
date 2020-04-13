using DBLite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DBLite.ViewModels
{
    class ListViewModel : BaseViewModel
    {
        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        private AppDbContext _db;

        public Command LoadCommand { get; set; }

        public ListViewModel()
        {
            _db = App.Db;
            LoadCommand = new Command(
                async () => {
                    IsBusy = true;
                    Students = new ObservableCollection<Student>(await _db.GetItemsAsync());
                    IsBusy = false;
                }
            );
        }

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }      
    }
}
