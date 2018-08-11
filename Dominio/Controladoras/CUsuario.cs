using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Controladoras
{
    public class CUsuario
    {
        #region Singleton
        private static CUsuario _instancia = null;
        private static readonly object bloqueo = new Object();

        private CUsuario() { }

        public static CUsuario Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CUsuario();

                    return _instancia;
                }
            }
        }
        #endregion
        public List<Usuario> _Usuarios { get; set; }
        public List<Chef> _Chef { get; set; }

        public bool AltaChef(string pUsername, string pPassword, Usuario.Rol pRol, Documento pDocumento, string pNombre, string pApellido, decimal pSueldo)
        {
            bool ret = false;

            if (ValidarData(pUsername, pPassword, pRol, pDocumento, pNombre, pApellido, pSueldo))
            {
                Chef c = BuscarChef(pDocumento);
                if (c == null) {
                    Usuario u = BuscarUsuario(pUsername);
                    if (u == null)
                    {
                        c = new Chef()
                        {
                            Username = pUsername,
                            Password = pPassword,
                            Nombre = pNombre,
                            Apellido = pApellido,
                            Documento = pDocumento,
                            UserRole = pRol,
                            Sueldo = pSueldo,
                            FechaIngreso = DateTime.Today
                        };
                        _Chef.Add(c);
                        _Usuarios.Add(c);
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public Chef BuscarChef(Documento pDocumento)
        {
            Chef c = null;
            int contador = 0;

            while(c == null && contador < _Chef.Count)
            {
                if (_Chef[contador].Documento == pDocumento)
                {
                    c = _Chef[contador];
                }
                contador++;
            }

            return c;
        }

        public Usuario BuscarUsuario(string pUsername)
        {
            Usuario u = null;
            int contador = 0;

            while (u == null && contador < _Usuarios.Count)
            {
                if (_Usuarios[contador].Username == pUsername)
                {
                    u = _Usuarios[contador];
                }
                contador++;
            }
            return u;
        }

        public bool Login(string pUsername, string pPassword)
        {
            bool ret = false;

            if (ValidarData(pUsername, pPassword))
            {
                Usuario u = ValidarUsuario(pUsername, pPassword);

                if (u != null)
                    ret = true;
            }

            return ret;
        }

        public Usuario.Rol RolAsociado(string pRol)
        {
            Usuario.Rol ret = (Usuario.Rol)Enum.Parse(typeof(Usuario.Rol), pRol);

            return ret;
        }

        public bool ValidarData(string pUsername, string pPassword)
        {
            return (pUsername != "" && pPassword != "");
        }

        public bool ValidarData(string pUsername, string pPassword, Usuario.Rol pRol, Documento pDocumento, string pNombre, string pApellido, decimal pSueldo)
        {
            return (pUsername != "" &&
                        pPassword != "" &&
                            pRol != null &&
                                pDocumento != null &&
                                    pNombre != "" &&
                                        pApellido != "" &&
                                            pSueldo > 0);
        }

        public Usuario ValidarUsuario(string pUsername, string pPassword)
        {
            Usuario u = null;
            int contador = 0;

            while (u == null && contador < _Usuarios.Count)
            {
                if (_Usuarios[contador].Username == pUsername && _Usuarios[contador].Password == pPassword)
                    u = _Usuarios[contador];

                contador++;
            }

            return u;
        }
    }
}
