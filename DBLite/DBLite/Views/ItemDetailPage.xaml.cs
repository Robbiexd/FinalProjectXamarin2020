using DBLite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DBLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        private DetailViewModel _vm;
        public ItemDetailPage(DetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _vm = viewModel;
        }
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        async void Delete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirm", "Are you sure you want to remove student " + _vm.Student.Lastname + " from database?", "Yes", "No");
            if (answer)
            {
                MessagingCenter.Send(this, "DeleteStudent", _vm.Student.Id);
                MessagingCenter.Send(this, "UpdateStudents");
                await Navigation.PopAsync();
            }
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new NavigationPage(
                    new EditItemPage(new EditViewModel(_vm.Student, _vm.Classrooms))
                )
            );
        }
    }
}