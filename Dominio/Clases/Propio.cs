using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Propio : Menu
    {
        public List<IngredientesPorMenu> Ingredientes { get; set; }
        public Chef Chef { get; set; }
        public decimal Ganancia { get; set; }

        public override decimal CalcularPrecioVenta()
        {
            throw new NotImplementedException();
        }
    }
}
