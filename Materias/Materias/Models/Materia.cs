using System;
using System.Collections.Generic;
using System.Text;

namespace Materias.Models
{
    public enum Status
    {
        Aprovado,
        Cursando,
        Cursar
    }
    public class Materia
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Professor { get; set; }
        public int Periodo { get; set; }
        public int Nota { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Observacoes { get; set; }
        public Status Status { get; set; }

        public string DataInicioStr { get => DataInicio.ToString("dd/MM/yyyy"); }
        public string DataFimStr { get => DataFim.ToString("dd/MM/yyyy"); }

    }
}
