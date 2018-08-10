using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class PreElaborado : Menu
    {
        public string Proveedor { get; set; }
        public decimal Costo { get; set; }
        public static double Ganancia { get; set; } //porcentaje
        public override decimal CalcularPrecioVenta()
        {
            throw new NotImplementedException();
        }
    }
}
