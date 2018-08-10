using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Ingrediente
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Activo { get; set; }
    }
}
