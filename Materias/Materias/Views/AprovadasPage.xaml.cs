using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Materias.ViewModels;

namespace Materias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AprovadasPage : ContentPage
    {
        AprovadoViewModel _viewModel;

        public AprovadasPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AprovadoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}