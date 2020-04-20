using DBLite.Models;
using DBLite.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBLite.ViewModels
{
    public class DetailViewModel : BaseViewModel
    {
        private Student _student;

        public DetailViewModel()
        {
            Title = "";
        }
        public DetailViewModel(Student item = null) : this()
        {            
            Student = item;
            Title = item?.Lastname;
        }

        public Student Student 
        { 
            get { return _student; } 
            set { SetProperty(ref _student, value); } 
        }
    }
}
