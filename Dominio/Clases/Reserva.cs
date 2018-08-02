using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Reserva
    {
        public string sNombre { get; set; }
        public int nCantPersonas { get; set; }
        public DateTime dFechaReserva { get; set; }
        public List<Menu> lMenues { get; set; }
        public Mesa oMesa { get; set; }
    }
}
