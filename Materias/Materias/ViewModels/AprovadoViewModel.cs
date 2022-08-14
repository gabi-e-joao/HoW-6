using Materias.Models;
using Materias.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Materias.ViewModels
{
    public class AprovadoViewModel : BaseViewModel
    {
        private Materia _selectedMateria;

        public ObservableCollection<Materia> Materias { get; }
        public Command LoadMateriasViewModel { get; }
        public Command AddMateriaCommand { get; }
        public Command<Materia> ItemTapped { get; }

        public AprovadoViewModel()
        {
            Materias = new ObservableCollection<Materia>();

            LoadMateriasViewModel = new Command(async () => await ExecuteLoadMateriaCommand());
            ItemTapped = new Command<Materia>(OnItemSelected);
            AddMateriaCommand = new Command(OnAddItem);
        }

        //Popula a coleção de materias
        async Task ExecuteLoadMateriaCommand()
        {
            IsBusy = true;

            try
            {
                Materias.Clear();
                foreach (var materia in await MateriaStore.GetItemsAsync(Status.Aprovado))
                    Materias.Add(materia);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Função executada quando a view é apresentada
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedMateria = null;
        }

        //Nome selecionado
        public Materia SelectedMateria
        {
            get => _selectedMateria;
            set
            {
                SetProperty(ref _selectedMateria, value);
                OnItemSelected(value);
            }
        }

        //Função executada ao apertar o botão de adicionar registro
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CadastroAprovadoPage));
        }

        //Função executada ao selecionar uma materia
        async void OnItemSelected(Materia materia)
        {
            if (materia == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CadastroAprovadoPage)}?{nameof(Materia.Id)}={materia.Id}");
        }
    }
}
