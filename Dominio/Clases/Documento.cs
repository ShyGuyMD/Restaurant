using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Documento
    {
        public enum TipoDocumento { Cedula, Pasaporte, Otros };
        public string sNumero { get; set; }
        public TipoDocumento oTipoDocumento { get; set; }
    }
}
