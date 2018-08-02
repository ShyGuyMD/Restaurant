using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public abstract class Menu
    {
        public int nId { get; set; }
        public string sDescripcion { get; set; }
        public decimal nPrecio { get; set; }
        public decimal nPrecioVenta { get; set; }
        public static int nUltimoId { get; set; }

        public abstract decimal CalcularPrecioVenta();
    }
}
