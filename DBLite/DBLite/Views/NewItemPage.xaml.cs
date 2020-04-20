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
        private List<string> _classIndexes = new List<string>();
        private ListViewModel _vm;
        public Student Student { get; set; } = new Student();

        public NewItemPage(ListViewModel vm)
        {           
            InitializeComponent();
            Title = "Add Student";
            _vm = vm;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            Student.ClassroomId = "L1";
            MessagingCenter.Send(this, "AddStudent", Student);
            MessagingCenter.Send(this, "UpdateStudents");
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.LoadClassesCommand.Execute(null);
            _classIndexes.Clear();
            ClassPicker.Items.Clear();
            foreach (var cr in _vm.Classrooms)
            {
                ClassPicker.Items.Add(cr.Name);
                _classIndexes.Add(cr.Id);
            }
        }
    }
}