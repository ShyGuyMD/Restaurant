using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class Mesa
    {
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public string Ubicacion { get; set; }
        public bool Activo { get; set; }

        public Mesa()
        {
            Activo = true;
        }

        public override string ToString()
        {
            return Numero + "";
        }
    }
}
