using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Clases
{
    public class Usuario
    {
        public enum Rol { Administrador, Chef };

        public string sUsername { get; set; }
        public string sPassword { get; set; }
        public Rol oRol { get; set; } 
    }
}
