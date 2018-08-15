using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public abstract class Menu
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public static int UltimoId { get; set; }
        public bool Activo { get; set; }

        public abstract decimal CalcularPrecioVenta();
    }
}
