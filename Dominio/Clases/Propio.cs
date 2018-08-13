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
        public int HorasElaboracion { get; set; }
        public decimal Ganancia { get; set; } //porcentaje

        public Propio()
        {
            Activo = true;
        }

        public override decimal CalcularPrecioVenta()
        {
            decimal precio = 0;
            foreach(IngredientesPorMenu ipm in Ingredientes)
                precio += ipm.CalcularCostoIngrediente();

            precio += Chef.CalcularSalarioHora();
            precio += precio * Ganancia / 100;

            return precio;
        }
    }
}
