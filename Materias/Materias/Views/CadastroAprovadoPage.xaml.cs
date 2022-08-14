using Materias.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Materias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroAprovadoPage : ContentPage
    {
        CadastroAprovadoViewModel _viewModel;

        public CadastroAprovadoPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CadastroAprovadoViewModel();
        }
    }
}