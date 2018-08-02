using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Controladoras
{
    public class CMesa
    {
        #region Singleton
        private static CMesa _instancia = null;
        private static readonly object bloqueo = new Object();

        private CMesa() { }

        public static CMesa Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CMesa();
                    
                    return _instancia;
                }
            }
        }
        #endregion
        public List<Mesa> _Mesas { get; set; }

        public bool AltaMesa(int pNumero, int pCapacidad, string pUbicacion)
        {
            throw new NotImplementedException();
        }
    }
}
