using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
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

        public void ActualizarIngrediente(Ingrediente i, int cantidad)
        {
            bool encontrado = false;
            int contador = 0;
            while (contador < Ingredientes.Count && !encontrado)
            {
                if (Ingredientes[contador].Ingrediente == i)
                {
                    Ingredientes[contador].Cantidad = cantidad;
                    encontrado = true;
                }
                contador++;
            }
        }

        public bool TieneIngrediente(Ingrediente i)
        {
            bool encontrado = false;
            int contador = 0;
            while (contador < Ingredientes.Count && !encontrado)
            {
                if (Ingredientes[contador].Ingrediente == i)
                    encontrado = true;
                contador++;
            }

            return encontrado;
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
