﻿using Materias.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace Materias.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class CadastroCursandoViewModel : BaseViewModel
    {
        private int id;
        private string nome;
        private string professor;
        private string observacoes;
        private DateTime dataInicio = DateTime.Now;
        private int periodo;

        public CadastroCursandoViewModel()
        {
            SalvarCommand = new Command(OnSave, ValidaDados);
            this.PropertyChanged += (_, __) => SalvarCommand.ChangeCanExecute();
            Title = "Novo curso em andamento";
        }

        private bool ValidaDados()
        {
            return !string.IsNullOrWhiteSpace(nome) && !string.IsNullOrWhiteSpace(professor) && (periodo > 0);
        }

        public string Id
        {
            get { return id.ToString(); }
            set
            {
                id = int.Parse(value);
                LoadMateria(id);
            }
        }

        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }

        public string Professor
        {
            get => professor;
            set => SetProperty(ref professor, value);
        }

        public string Observacoes
        {
            get => observacoes;
            set => SetProperty(ref observacoes, value);
        }

        public DateTime DataInicio
        {
            get => dataInicio;
            set => SetProperty(ref dataInicio, value);
        }

        public int Periodo
        {
            get => periodo;
            set => SetProperty(ref periodo, value);
        }

        public Command SalvarCommand { get; }
        public Command DeletaCommand { get; }

        private async void OnSave()
        {
            Materia materia = new Materia()
            {
                Id = id,
                Nome = Nome,
                Professor = Professor,
                Observacoes = Observacoes,
                DataInicio = DataInicio,
                Periodo = Periodo,
                Status = Status.Cursando,
            };

            if (id != 0)
                await MateriaStore.UpdateItemAsync(materia);
            else
            {
                materia.Id = await MateriaStore.GetNewId();
                await MateriaStore.AddItemAsync(materia);
            }

            await Shell.Current.GoToAsync("..");
        }

        public async void LoadMateria(int materiaId)
        {
            try
            {
                var materia = await MateriaStore.GetItemAsync(materiaId);
                Nome = Title = materia.Nome;
                Professor = materia.Professor;
                Observacoes = materia.Observacoes;
                DataInicio = materia.DataInicio;
                Periodo = materia.Periodo;
            }
            catch (Exception)
            {
                Debug.WriteLine("Falha ao excluir a materia");
            }
        }
    }
}
