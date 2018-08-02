using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class PreElaborado : Menu
    {
        public string sProveedor { get; set; }
        public decimal nCosto { get; set; }
        public static double nGanancia { get; set; } //porcentaje
        public override decimal CalcularPrecioVenta()
        {
            throw new NotImplementedException();
        }
    }
}
