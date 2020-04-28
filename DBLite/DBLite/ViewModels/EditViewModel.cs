using DBLite.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DBLite.ViewModels
{
    public class EditViewModel: BaseViewModel
    {
        private Student _student;
        private Classroom _selectedClassroom;
        private int? _selectedClassroomIndex;
        private ObservableCollection<Classroom> _classrooms;
        private ObservableCollection<string> _classroomNames;
        private ObservableCollection<int> _classroomKeys;
        public EditViewModel()
        {
            Title = "";
        }
        public EditViewModel(Student item, ObservableCollection<Classroom> classrooms) : this()
        {
            Student = item;
            Classrooms = classrooms;
            ClassroomNames = new ObservableCollection<string>();
            ClassroomKeys = new ObservableCollection<int>();
            foreach (var cr in Classrooms)
            {
                ClassroomNames.Add(cr.Name);
                ClassroomKeys.Add(cr.Id);
            }
            SelectedClassroomIndex = ClassroomKeys.IndexOf(Student.ClassroomId);
            Title = item?.Lastname;
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
        public Classroom SelectedClassroom
        {
            get { return _selectedClassroom; }
            set { SetProperty(ref _selectedClassroom, value); }
        }
        public int? SelectedClassroomIndex
        {
            get { return _selectedClassroomIndex; }
            set { SetProperty(ref _selectedClassroomIndex, value); }
        }
        public ObservableCollection<string> ClassroomNames
        {
            get { return _classroomNames; }
            set { SetProperty(ref _classroomNames, value); }
        }
        public ObservableCollection<int> ClassroomKeys
        {
            get { return _classroomKeys; }
            set { SetProperty(ref _classroomKeys, value); }
        }
    }
}
