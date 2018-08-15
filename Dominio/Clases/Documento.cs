using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    [Serializable]
    public class Documento
    {
        public enum TipoDocumento { Cedula = 1, Pasaporte = 2, Otros = 3 };
        public string Numero { get; set; }
        public TipoDocumento TipoDoc { get; set; }

        public Documento() { }

        public override string ToString()
        {
            return TipoDoc.ToString() + " " + Numero;
        }

        public override bool Equals(object obj)
        {
            return (TipoDoc == ((Documento)obj).TipoDoc &&
                Numero == ((Documento)obj).Numero);
        }
    }
}
