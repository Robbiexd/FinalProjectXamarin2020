using DBLite.Models;
using DBLite.Services;
using DBLite.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace DBLite.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private Student _student;
        private ObservableCollection<Classroom> _classrooms;

        public DetailViewModel()
        {
            Title = "";
        }
        public DetailViewModel(Student item, ObservableCollection<Classroom> classrooms) : this()
        {            
            Student = item;
            Classrooms = classrooms;
            Title = item?.Lastname;
            MessagingCenter.Subscribe<EditItemPage,Student>(this, "UpdateStudent", (sender, student) =>
            {
                Student = student;
            });
        }

        public Student Student 
        { 
            get { return _student; } 
            set { SetProperty(ref _student, value); } 
        }

        public ObservableCollection<Classroom> Classrooms
        {
            get { return _classrooms; }
            set { SetProperty(ref _classrooms, value); }
        }
    }
}
