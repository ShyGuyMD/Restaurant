using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class Ingrediente
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Activo { get; set; }

        public Ingrediente()
        {
            Activo = true;
        }

        public override string ToString()
        {
            return String.Format("Codigo: {0}\nDescripcion: {1}\nCosto {2}\nActivo: {3}", Codigo, Descripcion, Costo, Activo);
        }
    }
}
