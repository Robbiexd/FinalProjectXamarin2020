using DBLite.Models;
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
    public partial class ItemsPage : ContentPage
    {
        private ListViewModel _vm;
        public ItemsPage()
        {
            InitializeComponent();
            _vm = (ListViewModel)this.BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm.Students.Count == 0)
            {
                _vm.LoadCommand.Execute(null);
            }
        }

        protected async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Student;
            if (item == null)
                return;

            _vm.LoadClassesCommand.Execute(null);

            await Navigation.PushAsync(new ItemDetailPage(new DetailViewModel(item, _vm.Classrooms)));

            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            _vm.LoadClassesCommand.Execute(null);
            Dictionary<int, string> classroomList = new Dictionary<int, string>();
            foreach(var cr in _vm.Classrooms)
            {
                classroomList.Add(cr.Id,cr.Name);
            }
            await Navigation.PushModalAsync(
                new NavigationPage(
                    new NewItemPage(classroomList)
                )
            );
        }
    }
}