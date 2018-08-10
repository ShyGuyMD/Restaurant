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

        public bool Alta(string pNombre, int pCantPersonas, DateTime pFechaReserva, List<Menu> pMenues, Mesa pMesa)
        {
            bool ret = false;

            if (ValidarData(pNombre, pCantPersonas, pFechaReserva, pMesa))
            {
                Reserva r = Buscar(pNombre, pFechaReserva, pMesa);
                if (r == null)
                {
                    r = new Reserva()
                    {
                        Nombre = pNombre,
                        CantPersonas = pCantPersonas,
                        FechaReserva = pFechaReserva,
                        Mesa = pMesa
                    };
                    if (pMenues != null && pMenues.Count > 0)
                        r.Menues = pMenues;

                    _Reservas.Add(r);
                    ret = true;
                    
                }
            }

            return ret;
        }

        public bool Baja(string pNombre, DateTime pFechaReserva, int pIdMesa)
        {
            throw new NotImplementedException();
        }

        public Reserva Buscar(string pNombre, DateTime pFechaReserva, Mesa pMesa)
        {
            Reserva r = null;
            int contador = 0;
            while (r == null && contador < _Reservas.Count)
            {
                Reserva aux = _Reservas[contador];
                if (aux.Nombre == pNombre &&
                        aux.FechaReserva == pFechaReserva &&
                            aux.Mesa == pMesa)
                    r = aux;

                contador++;
            }

            return r;
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            throw new NotImplementedException();
        }

        public bool ValidarData(string pNombre, int pCantPersonas, DateTime pFechaReserva, Mesa pMesa)
        {
            return (pNombre != "" && 
                        pCantPersonas > 0 && 
                            pFechaReserva > DateTime.Now && 
                                pMesa != null);
        }
    }
}
