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

        public string Username { get; set; }
        public string Password { get; set; }
        public Rol UserRole { get; set; } 

        public Usuario() { }

        public override string ToString()
        {
            return String.Format("Username: {0}\nPassword: {1}\nRol: {2}", Username, Password, UserRole.ToString());
        }
    }
}
