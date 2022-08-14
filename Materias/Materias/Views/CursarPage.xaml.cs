using Materias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Materias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CursarPage : ContentPage
    {
        CursarViewModel _viewModel;

        public CursarPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CursarViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}