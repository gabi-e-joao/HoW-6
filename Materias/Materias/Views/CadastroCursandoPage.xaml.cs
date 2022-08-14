using Materias.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Materias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroCursandoPage : ContentPage
    {
        CadastroCursandoViewModel _viewModel;

        public CadastroCursandoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CadastroCursandoViewModel();
        }
    }
}