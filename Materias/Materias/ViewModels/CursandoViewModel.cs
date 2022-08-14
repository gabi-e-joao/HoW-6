using Materias.Models;
using Materias.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Materias.ViewModels
{
    public class CursandoViewModel : BaseViewModel
    {
        private Materia _selectMateria;

        public ObservableCollection<Materia> Materias { get; }
        public Command LoadMateriasCommand { get; }
        public Command AddMateriasCommand { get; }
        public Command<Materia> ItemTapped { get; }
        public Command DeletaCommand { get; }
        public Command FinalizaMateriaCommand { get; }

        public CursandoViewModel()
        {
            Materias = new ObservableCollection<Materia>();

            LoadMateriasCommand = new Command(async () => await ExecuteLoadMateriaCommand());
            ItemTapped = new Command<Materia>(OnItemSelected);
            AddMateriasCommand = new Command(OnAddItem);
            DeletaCommand = new Command<Materia>(OnDelete);
            FinalizaMateriaCommand = new Command<Materia>(FinalizaMateria);
        }

        //Popula a coleção de materias
        async Task ExecuteLoadMateriaCommand()
        {
            IsBusy = true;

            try
            {
                Materias.Clear();
                foreach (var materia in await MateriaStore.GetItemsAsync(Status.Cursando))
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

        //Função que deleta uma materia
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

        //Função executada quando a view é apresentada
        public async void OnAppearing()
        {
            IsBusy = true;
            SelectMateria = null;
            await ExecuteLoadMateriaCommand();
        }

        //Nome selecionado
        public Materia SelectMateria
        {
            get => _selectMateria;
            set
            {
                SetProperty(ref _selectMateria, value);
                OnItemSelected(value);
            }
        }

        //Função executada ao apertar o botão de adicionar registro
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CadastroCursandoPage));
        }

        //Função executada ao selecionar uma materia
        async void OnItemSelected(Materia materia)
        {
            if (materia == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CadastroCursandoPage)}?{nameof(Materia.Id)}={materia.Id}");
        }

        async void FinalizaMateria(Materia materia)
        {
            if (materia == null)
                return;

            materia.Status = Status.Aprovado;
            materia.DataFim = DateTime.Now;
            await MateriaStore.UpdateItemAsync(materia);
            await ExecuteLoadMateriaCommand();
        }
    }
}
