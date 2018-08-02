using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;

namespace Dominio.Controladoras
{
    public sealed class CMenu
    {
        #region Singleton
        private static CMenu _instancia = null;
        private static readonly object bloqueo = new Object();

        private CMenu() { }

        public static CMenu Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new CMenu();

                    return _instancia;
                }
            }
        }
        #endregion

        public List<Menu> _Menues { get; set; }

        public bool AltaMenuPreelaborado(string pProveedor, decimal pCosto, string pDesc)
        {
            throw new NotImplementedException();
        }

        public bool AltaMenuPropio(int pIdChef, Dictionary<int, int> pIngredientes, double pGanancia, string pDesc)
        {
            throw new NotImplementedException();
        }

        public List<Menu> ListarMenues()
        {
            throw new NotImplementedException();
        }
        // Usar Diccionario en caso de querer hacer las cosas bien.
        public bool ModificarMenu(int pIdMenu, int pIdIngrediente, int pCantidad)
        {
            throw new NotImplementedException();
        }
        public List<Menu> ListadoMenuesConIngrediente(int pIdIngrediente)
        {
            throw new NotImplementedException();
        }

        public void CargarGananciaMenuPreelaborado(double ganancia)
        {
            PreElaborado.nGanancia = ganancia;
        }
    }
}
