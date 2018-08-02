using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Controladoras
{
    public class CReserva
    {
        #region Singleton
        private static CReserva _instancia = null;
        private static readonly object bloqueo = new Object();

        private CReserva() { }

        public static CReserva Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CReserva();

                    return _instancia;
                }
            }
        }
        #endregion
        public List<Reserva> _Reservas { get; set; }

        public bool AltaReserva(string pNombrePersona, int pCantPersonas, DateTime pFechaReserva, List<int> pIdMenues, int pNumeroMesa)
        {
            throw new NotImplementedException();
        }

        public bool BajaReserva(string pNombre, DateTime pFechaReserva, int pIdMesa)
        {
            throw new NotImplementedException();
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            throw new NotImplementedException();
        }
    }
}
