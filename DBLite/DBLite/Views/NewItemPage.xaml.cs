using DBLite.Models;
using DBLite.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DBLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        private List<int> _classIndexes = new List<int>();
        private Dictionary<int,string> _classrooms;
        public Student Student { get; set; } = new Student();
        public ObservableCollection<string> Classes { get; set; } = new ObservableCollection<string>();

        public int? SelectedClassroom { get; set; }
        public NewItemPage(Dictionary<int, string> classroomsList)
        {           
            InitializeComponent();
            Title = "Add new student";
            _classrooms = classroomsList;
            _classIndexes.Clear();
            Classes.Clear();
            foreach (var cr in classroomsList)
            {
                Classes.Add(cr.Value);
                _classIndexes.Add(cr.Key);
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (
                String.IsNullOrEmpty(Student.Firstname) || 
                String.IsNullOrEmpty(Student.Lastname) || 
                SelectedClassroom == null
               )
            {
                await DisplayAlert("Warning", "Incomplete data", "Ok");
            }
            else
            {
                Student.ClassroomId = _classIndexes[(int)SelectedClassroom];
                MessagingCenter.Send(this, "AddStudent", Student);
                MessagingCenter.Send(this, "UpdateStudents");
                await Navigation.PopModalAsync();
            }           
        }
    }
}