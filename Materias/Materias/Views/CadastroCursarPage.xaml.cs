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
    public partial class CadastroCursarPage : ContentPage
    {
        CadastroCursarViewModel _viewModel;

        public CadastroCursarPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CadastroCursarViewModel();
        }
    }
}