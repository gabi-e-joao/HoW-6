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
    public class CursarViewModel : BaseViewModel
    {
        private Materia _selectMateria;

        public ObservableCollection<Materia> Materias { get; }
        public Command LoadMateriasCommand { get; }
        public Command AddMateriaCommand { get; }
        public Command<Materia> ItemTapped { get; }

        public Command DeletaCommand { get; }
        public Command IniciarMateriaCommand { get; }

        public CursarViewModel()
        {
            Materias = new ObservableCollection<Materia>();

            LoadMateriasCommand = new Command(async () => await ExecuteLoadMateriaCommand());
            ItemTapped = new Command<Materia>(OnItemSelected);
            AddMateriaCommand = new Command<Materia>(OnAddItem);
            DeletaCommand = new Command<Materia>(OnDelete);
            IniciarMateriaCommand = new Command<Materia>(IniciarMateria);
        }

        //Popula a coleção de materias
        async Task ExecuteLoadMateriaCommand()
        {
            IsBusy = true;

            try
            {
                Materias.Clear();
                foreach (var materia in await MateriaStore.GetItemsAsync(Status.Cursar))
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
            SelectedItem = null;
        }

        //Nome selecionado
        public Materia SelectedItem
        {
            get => _selectMateria;
            set
            {
                SetProperty(ref _selectMateria, value);
                OnItemSelected(value);
            }
        }

        //Função que deleta uma matéria
        private async void OnDelete(Materia materia)
        {
            try
            {
                if (materia.Id != 0)
                    await MateriaStore.DeleteItemAsync(materia.Id);

                await ExecuteLoadMateriaCommand();
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha ao excluir a materia");
            }
        }

        //Função executada ao apertar o botão de adicionar registro
        private async void OnAddItem(Materia materia)
        {
            await Shell.Current.GoToAsync(nameof(CadastroCursarPage));
        }

        //Função executada ao selecionar uma materia
        async void OnItemSelected(Materia materia)
        {
            if (materia == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CadastroCursarPage)}?{nameof(Materia.Id)}={materia.Id}");
        }

        async void IniciarMateria(Materia materia)
        {
            if (materia == null)
                return;

            materia.Status = Status.Cursando;
            materia.DataInicio = DateTime.Now;
            await MateriaStore.UpdateItemAsync(materia);
            await ExecuteLoadMateriaCommand();
        }
    }
}
