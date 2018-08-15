using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class PreElaborado : Menu
    {
        public string Proveedor { get; set; }
        public decimal Costo { get; set; }
        public static decimal Ganancia { get; set; } //porcentaje

        public PreElaborado() {
            Activo = true;
        }

        public override decimal CalcularPrecioVenta()
        {
            return Costo + (Math.Round((Costo * Ganancia / 100), 2));
        }
    }
}
