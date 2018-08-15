using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class Reserva : IComparable<Reserva>
    {
        public string CodigoReserva { get; set; }
        public string Nombre { get; set; }
        public int CantPersonas { get; set; }
        public DateTime FechaReserva { get; set; }
        public List<Menu> Menues { get; set; }
        public Mesa Mesa { get; set; }

        public Reserva()
        {
        }

        public int CompareTo(Reserva other)
        {
            return CantPersonas.CompareTo(other.CantPersonas);
        }
    }
}
