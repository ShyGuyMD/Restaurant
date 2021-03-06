﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using Helpers;
using static Helpers.Utils;

namespace Dominio.Controladoras
{
    [Serializable]
    public class CReserva:ISerializable
    {
        #region Singleton
        private static CReserva _instancia = null;
        private static readonly object bloqueo = new Object();

        private CReserva() {
            _Reservas = new List<Reserva>();
        }

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

        public string Alta(string pNombre, int pCantPersonas, DateTime pFechaReserva, List<Menu> pMenues, Mesa pMesa)
        {
            string ret = "";

            if (ValidarData(pNombre, pCantPersonas, pFechaReserva, pMesa))
            {
                Reserva r = new Reserva()
                {
                    Nombre = pNombre,
                    CantPersonas = pCantPersonas,
                    FechaReserva = pFechaReserva,
                    Mesa = pMesa
                };
                r.CodigoReserva = Utils.Get.GenerarCodigo(6);
                if (pMenues != null && pMenues.Count > 0)
                    r.Menues = pMenues;

                _Reservas.Add(r);
                ret = r.CodigoReserva;   
            }

            return ret;
        }

        public ExitCode Baja(string pCodReserva)
        {
            var exit = ExitCode.NO_RESERVATION_ERROR;

            if (pCodReserva != "")
            {
                Reserva r = BuscarPorCodigo(pCodReserva);
                if (r != null)
                {
                    _Reservas.Remove(r);
                    exit = ExitCode.OK;
                }
            }
            else
                exit = ExitCode.INPUT_DATA_ERROR;

            return exit;
        }

        public Reserva BuscarPorCodigo(string pCodReserva)
        {
            Reserva r = null;
            int contador = 0;
            while (r == null && contador < _Reservas.Count)
            {
                if (_Reservas[contador].CodigoReserva == pCodReserva)
                    r = _Reservas[contador];

                contador++;
            }

            return r;
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            List<Reserva> ret = new List<Reserva>();

            foreach (Reserva r in _Reservas)
                if (r.FechaReserva == pFechaReserva)
                    ret.Add(r);

            ret.Sort();
            return ret;
        }

        public bool ValidarData(string pNombre, int pCantPersonas, DateTime pFechaReserva, Mesa pMesa)
        {
            return (pNombre != "" && 
                        pCantPersonas > 0 && 
                            pFechaReserva > DateTime.Now && 
                                pMesa != null);
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listaReservas", this._Reservas, typeof(List<Reserva>));
        }

        public CReserva (SerializationInfo info, StreamingContext context)
        {
            this._Reservas = info.GetValue("listaReservas", typeof(List<Reserva>)) as List<Reserva>;
            CReserva._instancia = this;
        }
    }
}
