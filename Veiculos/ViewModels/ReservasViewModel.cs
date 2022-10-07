using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veiculos.ViewModels
{
    public class ReservasViewModel
    {
        public ReservasViewModel(string id, string dtInicio, string dtFim, string carroId)
        {
            Id = id;
            DtInicio = dtInicio;
            DtFim = dtFim;
            CarroId = carroId;
        }

        public string Id { get; set; }
        public string DtInicio { get; set; }
        public string DtFim { get; set; }
        public string CarroId { get; set; }
    }
}
