using System;
using System.Collections.Generic;
using System.Text;

namespace Veiculo.Dominio
{
    public class Agendador
    {
        public int Id { get; set; }
        public string Minute { get; set; }
        public string Hour { get; set; }
        public string DayMonth { get; set; }
        public string Month { get; set; }
        public string DayWeek { get; set; }
        public int Create_uid { get; set; }

    }
}
