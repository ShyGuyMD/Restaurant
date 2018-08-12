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

        private CMesa() {
            _Mesas = new List<Mesa>();
        }

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

        public bool Alta(int pNumero, int pCapacidad, string pUbicacion)
        {
            bool ret = false;

            if (ValidarData(pNumero, pCapacidad, pUbicacion))
            {
                Mesa m = Buscar(pNumero);
                if (m != null)
                {
                    m = new Mesa()
                    {
                        Numero = pNumero,
                        Capacidad = pCapacidad,
                        Ubicacion = pUbicacion
                    };
                    _Mesas.Add(m);
                    ret = true;
                }
            }

            return ret;
        }
        /*
        public bool Baja(int pNumero)
        {
            bool ret = false;
            Mesa m = Buscar(pNumero);

            if (m != null)
            {
                m.Activo = false;
                ret = true;
            }

            return ret;
        }

        public Mesa Modificar(int pNumero, int pCapacidad, string pUbicacion)
        {
            Mesa m = null;
            if (ValidarData(pNumero, pCapacidad, pUbicacion))
            {
                m = Buscar(pNumero);
                if (m != null)
                {
                    m.Capacidad = pCapacidad;
                    m.Ubicacion = pUbicacion;
                }
            }

            return m;
        }
        */
        public Mesa Buscar(int pNumero)
        {
            Mesa m = null;
            int contador = 0;
            while (m == null && contador < _Mesas.Count)
            {
                if (_Mesas[contador].Numero == pNumero && _Mesas[contador].Activo)
                    m = _Mesas[contador];

                contador++;
            }

            return m;
        }

        public bool ValidarData(int pNumero, int pCapacidad, string pUbicacion)
        {
            return (pNumero > 0 && pCapacidad > 0 && pUbicacion != "");
        }
    }
}
