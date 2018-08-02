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

        private CIngrediente() { }

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
            throw new NotImplementedException();
        }
        public bool BajaIngrediente(int pIdIngrediente)
        {
            throw new NotImplementedException();
        }
    }
}
