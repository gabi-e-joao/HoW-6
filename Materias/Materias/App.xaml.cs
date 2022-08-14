using Materias.Services;
using Materias.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Materias
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MateriaMockDataStore>();
            MainPage = new AppShell();
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
