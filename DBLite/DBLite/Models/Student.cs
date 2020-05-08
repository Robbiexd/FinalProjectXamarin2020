using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace DBLite.Models
{
    public class Student : INotifyPropertyChanged
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private int _classroomId;
        private string _phoneNumber;
        private string _email;

        [Key]
        public int Id { get { return _id; } set { _id = value; NotifyPropertyChanged(); } }
        [Required]
        public string Firstname { get { return _firstname; } set { _firstname = value; NotifyPropertyChanged(); } }
        [Required]
        public string Lastname { get { return _lastname; } set { _lastname = value; NotifyPropertyChanged(); } }
        [Required]
        public int ClassroomId { get { return _classroomId; } set { _classroomId = value; NotifyPropertyChanged(); NotifyPropertyChanged("Classroom"); } }
        //[Required]
        //public string Email { get { return _email; } set { _email = value; NotifyPropertyChanged(); } }
        //[Required]
        //public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; NotifyPropertyChanged(); } }
        public Classroom Classroom { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
