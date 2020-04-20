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

            await Navigation.PushAsync(new ItemDetailPage(new DetailViewModel(item)));

            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new NavigationPage(
                    new NewItemPage(_vm)
                )
            );
        }
    }
}