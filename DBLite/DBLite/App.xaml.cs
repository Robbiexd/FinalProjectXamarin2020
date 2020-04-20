using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DBLite.Services;
using DBLite.Views;
using DBLite.Models;

namespace DBLite
{
    public partial class App : Application
    {
        public static AppDbContext Db;

        public App(string dbPath)
        {
            InitializeComponent();
            Db = new AppDbContext(dbPath);
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
