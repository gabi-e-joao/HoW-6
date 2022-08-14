using Materias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materias.Services
{
    public class MateriaMockDataStore : IMateriaStore<Materia>
    {
        readonly List<Materia> materias;

        public MateriaMockDataStore()
        {
            materias = new List<Materia>()
            {
                new Materia
                { 
                    Id = 1,
                    Status = Status.Cursar,
                    Nome = "Programação para dispositivos móveis", 
                    Professor = "Luan Theo Sebastião Santos",
                    Periodo = 3,
                    Observacoes = "Observação." 
                },
                new Materia
                {
                    Id = 2,
                    Status = Status.Cursando,
                    Periodo = 5,
                    DataInicio = new DateTime(2021, 01, 29),
                    Nome = "Computação em nuvem", 
                    Professor = "Fabricio Bortoluzzi",
                    Observacoes = "Observação." 
                },
                new Materia
                {
                    Id = 3,
                    Status = Status.Aprovado,
                    Periodo = 1,
                    Nota = 9,
                    DataInicio = new DateTime(2021, 01, 29),
                    DataFim = new DateTime(2021, 02, 15),
                    Nome = "Tecnologias emergentes",
                    Professor = "Fabricio Bortoluzzi",
                    Observacoes = "Observação."
                },
            };
        }

        public async Task<bool> AddItemAsync(Materia materia)
        {
            materias.Add(materia);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Materia materia)
        {
            var oldMateria = materias.Where((Materia arg) => arg.Id == materia.Id).FirstOrDefault();
            materias.Remove(oldMateria);
            materias.Add(materia);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldMateria = materias.Where((Materia arg) => arg.Id == id).FirstOrDefault();
            materias.Remove(oldMateria);

            return await Task.FromResult(true);
        }

        public async Task<Materia> GetItemAsync(int id)
        {
            return await Task.FromResult(materias.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Materia>> GetItemsAsync(Status status)
        {
            return await Task.FromResult(materias.Where((materia) => materia.Status == status));
        }

        public async Task<int> GetNewId()
        {
            return await Task.FromResult(materias.OrderByDescending((x) => x.Id).FirstOrDefault().Id + 1);
        }
    }
}