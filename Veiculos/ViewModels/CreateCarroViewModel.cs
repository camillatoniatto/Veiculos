using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veiculos.ViewModels
{
    public class CreateCarroViewModel
    {
        public CreateCarroViewModel(string modelo, string placa, int ano, string estado)
        {
            Modelo = modelo;
            Placa = placa;
            Ano = ano;
            Estado = estado;
        }

        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public string Estado { get; set; }
    }
}
