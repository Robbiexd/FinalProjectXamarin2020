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
    public partial class EditItemPage : ContentPage
    {
        private EditViewModel _vm;
        public EditItemPage(EditViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _vm = viewModel;
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            if (
                String.IsNullOrEmpty(_vm.Student.Firstname) ||
                String.IsNullOrEmpty(_vm.Student.Lastname) ||
                _vm.SelectedClassroomIndex == null
               )
            {
                await DisplayAlert("Warning", "Incomplete data", "Ok");
            }
            else
            {
                _vm.Student.ClassroomId = _vm.ClassroomKeys[(int)_vm.SelectedClassroomIndex];
                await Task.Delay(100);
                await Task.Delay(100);
                await Task.Delay(100);
                await Navigation.PopModalAsync();
            }
        }
    }
}