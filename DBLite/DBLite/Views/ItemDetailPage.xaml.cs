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
    }
}