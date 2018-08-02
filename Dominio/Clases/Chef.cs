using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Chef : Usuario
    {
        public Documento oDocumento { get; set; }
        public string sNombre { get; set; }
        public string sApellido { get; set; }
        public DateTime dFechaIngreso { get; set; }
        public decimal nSueldo { get; set; }
    }
}
