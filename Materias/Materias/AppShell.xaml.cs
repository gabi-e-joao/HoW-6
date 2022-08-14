using Materias.ViewModels;
using Materias.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Materias
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CadastroCursandoPage), typeof(CadastroCursandoPage));
            Routing.RegisterRoute(nameof(CadastroCursarPage), typeof(CadastroCursarPage));
            Routing.RegisterRoute(nameof(CadastroAprovadoPage), typeof(CadastroAprovadoPage));
        }

    }
}
