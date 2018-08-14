using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using static Helpers.Utils;

namespace Dominio.Controladoras
{
    [Serializable]
    public class CMesa : ISerializable
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

        public ExitCode Alta(int pNumero, int pCapacidad, string pUbicacion)
        {
            var exit = ExitCode.EXISTING_TABLE_ERROR;

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
                    exit = ExitCode.OK;
                }
            }
            else
                exit = ExitCode.INPUT_DATA_ERROR;

            return exit;
        }
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

        public List<Mesa> ListarDisponibles(int pCapacidad)
        {
            List<Mesa> ret = new List<Mesa>();

            foreach (Mesa m in _Mesas)
                if (m.Activo && m.Capacidad >= pCapacidad)
                    ret.Add(m);

            return ret;
        }

        public bool ValidarData(int pNumero, int pCapacidad, string pUbicacion)
        {
            return (pNumero > 0 && pCapacidad > 0 && pUbicacion != "");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listaMesas", this._Mesas, typeof(List<Mesa>));
        }

        public CMesa(SerializationInfo info, StreamingContext context)
        {
            this._Mesas = info.GetValue("listaMesas", typeof(List<Mesa>)) as List<Mesa>;
            CMesa._instancia = this;
        }
    }
}
