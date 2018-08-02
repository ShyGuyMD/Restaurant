using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Propio : Menu
    {
        public List<IngredientesPorMenu> lIngredientes { get; set; }
        public Chef oChef { get; set; }
        public decimal nGanancia { get; set; }

        public override decimal CalcularPrecioVenta()
        {
            throw new NotImplementedException();
        }
    }
}
