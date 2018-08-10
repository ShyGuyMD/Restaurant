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

        public bool AltaChef(string pUsername, string pPassword, int pRol, Documento pDocumento, string pNombre, string pApellido, decimal pSueldo)
        {
            throw new NotImplementedException();
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

        public Usuario ValidarUsuario(string pUsername, string pPassword)
        {
            Usuario u = null;
            int contador = 0;

            while(u == null && contador < _Usuarios.Count)
            {
                if (_Usuarios[contador].Username == pUsername && _Usuarios[contador].Password == pPassword)
                    u = _Usuarios[contador];

                contador++;
            }

            return u;
        }

        public Chef Buscar(Documento pDocumento)
        {
            Chef c = null;
            int contador = 0;

            while(c == null && contador < _Chef.Count)
            {
                if (_Chef[contador].Documento == pDocumento)
                {

                }
            }

        }

        public bool ValidarData(string pUsername, string pPassword)
        {
            return (pUsername != "" && pPassword != "");
        }
    }
}
