using DBLite.Models;
using DBLite.Services;
using DBLite.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DBLite.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        private ObservableCollection<Classroom> _classrooms = new ObservableCollection<Classroom>();
        private AppDbContext _db;

        public Command LoadCommand { get; set; }
        public Command LoadClassesCommand { get; set; }

        public ListViewModel()
        {
            _db = App.Db;
            Title = "List";
            LoadCommand = new Command(
                async () => {
                    IsBusy = true;
                    Students = new ObservableCollection<Student>(await _db.GetItemsAsync());
                    IsBusy = false;
                }
            );
            LoadClassesCommand = new Command(
                async () => {
                    IsBusy = true;
                    Classrooms = new ObservableCollection<Classroom>(await _db.GetClassroomsAsync());
                    IsBusy = false;
                }
            );
            MessagingCenter.Subscribe<NewItemPage>(this, "UpdateStudents", (sender) =>
            {
                LoadCommand.Execute(null);
            });
            MessagingCenter.Subscribe<ItemDetailPage>(this, "UpdateStudents", (sender) =>
            {
                LoadCommand.Execute(null);
            });
            MessagingCenter.Subscribe<EditItemPage>(this, "UpdateStudents", (sender) =>
            {
                LoadCommand.Execute(null);
            });
            MessagingCenter.Subscribe<NewItemPage, Student>(this, "AddStudent", async (sender, student) =>
            {
                if (!await _db.AddItemAsync(student))
                    MessagingCenter.Send(this, "ShowAlert", "There was an error.");
            });
            MessagingCenter.Subscribe<EditItemPage, Student>(this, "EditStudent", async (sender, student) =>
            {
                if (!await _db.UpdateItemAsync(student))
                    MessagingCenter.Send(this, "ShowAlert", "There was an error.");
            });
            MessagingCenter.Subscribe<ItemDetailPage, int>(this, "DeleteStudent", async (sender, id) =>
            {
                if (!await _db.DeleteItemAsync(id))
                    MessagingCenter.Send(this, "ShowAlert", "There was an error.");
            });
        }

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set { SetProperty(ref _students, value); }
        }
        public ObservableCollection<Classroom> Classrooms
        {
            get { return _classrooms; }
            set { SetProperty(ref _classrooms, value); }
        }
    }
}
