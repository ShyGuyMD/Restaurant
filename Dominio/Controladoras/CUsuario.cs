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

        public bool AltaChef(string pUsername, string pPassword, int pRol, string sNumDoc, int pTipoDoc, string pNombre, string pApellido, decimal pSueldo)
        {
            throw new NotImplementedException();
        }
        public bool Login(string pUsername, string pPassword)
        {
            //throw new NotImplementedException();
            return true;
        }
    }
}
