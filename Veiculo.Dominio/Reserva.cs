using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veiculo.Dominio
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime DtInicio { get; set; }
        public DateTime DtFim { get; set; }
        public int CarroId { get; set; }
    }
}
