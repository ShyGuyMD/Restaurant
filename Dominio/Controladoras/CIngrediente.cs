using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Controladoras
{
    public class CIngrediente
    {
        #region Singleton
        private static CIngrediente _instancia = null;
        private static readonly object bloqueo = new Object();

        private CIngrediente() {
            _Ingredientes = new List<Ingrediente>();
        }

        public static CIngrediente Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CIngrediente();
                    
                    return _instancia;
                }
            }
        }
        #endregion

        public List<Ingrediente> _Ingredientes { get; set; }

        public bool AltaIngrediente(string pCodigo, string pDescripcion, decimal pCosto)
        {
            bool ret = false;

            if (ValidarData(pCodigo, pDescripcion, pCosto))
            {
                Ingrediente i = Buscar(pCodigo);
                if (i == null)
                {
                    i = new Ingrediente()
                    {
                        Codigo = pCodigo,
                        Descripcion = pDescripcion,
                        Costo = pCosto
                    };
                    _Ingredientes.Add(i);
                    ret = true;
                }
                else if (i != null && !i.Activo)
                {
                    i.Descripcion = pDescripcion;
                    i.Costo = pCosto;
                    i.Activo = true;
                    ret = true;
                }
            }

            return ret;
        }

        public IngredientesPorMenu ArmarObjetoIngrediente(Ingrediente i, int cantidad)
        {
            IngredientesPorMenu ret = new IngredientesPorMenu()
            {
                Ingrediente = i,
                Cantidad = cantidad
            };

            return ret;
        }

        public bool BajaIngrediente(string pCodigo)
        {
            bool ret = false;
            if (pCodigo != null)
            {
                Ingrediente i = Buscar(pCodigo);
                if (i != null)
                {
                    i.Activo = false;
                    ret = true;
                }
            }

            return ret;
        }

        public Ingrediente Buscar(string pCodigo)
        {
            Ingrediente ret = null;
            int contador = 0;

            while (contador < _Ingredientes.Count && ret == null)
            {
                if (_Ingredientes[contador].Codigo == pCodigo)
                {
                    ret = _Ingredientes[contador];
                }
                contador++;
            }

            return ret;
        }

        public Ingrediente BuscarActivo(string pCodigo)
        {
            Ingrediente ret = Buscar(pCodigo);
            if (!ret.Activo)
                ret = null;

            return ret;
        }

        public bool ValidarData(string pCodigo, string pDescripcion, decimal pCosto)
        {
            return (pCodigo != "" && pDescripcion != "" && pCosto > 0);
        }
    }
}
