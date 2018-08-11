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
            bool ret = false;

            if (ValidarData(pProveedor, pCosto, pDesc))
            {
                PreElaborado p = new PreElaborado()
                {
                    Id = Menu.UltimoId + 1,
                    Proveedor = pProveedor,
                    Costo = pCosto,
                    Descripcion = pDesc
                };
                p.PrecioVenta = p.CalcularPrecioVenta();
                Menu.UltimoId = p.Id;

                _Menues.Add(p);
                ret = true;
            }

            return ret;
        }

        public bool AltaMenuPropio(Chef pChef, List<IngredientesPorMenu> pIngredientes, double pGanancia, string pDesc)
        {
            bool ret = false;

            if (ValidarData(pChef, pIngredientes, pGanancia, pDesc))
            {
                Propio p = new Propio()
                {
                    Id = Menu.UltimoId + 1,
                    Chef = pChef,
                    Ingredientes = pIngredientes,
                    Ganancia = pGanancia,
                    Descripcion = pDesc
                };
                p.PrecioVenta = p.CalcularPrecioVenta();
                Menu.UltimoId = p.Id;
                _Menues.Add(p);
                ret = true;
            }

            return ret;
        }

        public Menu Buscar(int idMenu)
        {
            bool encontrado = false;
            int contador = 0;
            while (!encontrado && contador < _Menues.Count)
            {
                if (_Menues[contador].Id == idMenu)
                    return _Menues[contador];               // REVISAR ESTO CON LILIANA

                contador++;
            }
            return null;
        }

        public List<Menu> ListarMenues()
        {
            throw new NotImplementedException();
        }

        public bool ModificarMenu(Menu pMenu, List<IngredientesPorMenu> pIngredientes)
        {
            throw new NotImplementedException();
        }
        public List<Menu> ListadoMenuesConIngrediente(Ingrediente i)
        {
            throw new NotImplementedException();
        }

        public void CargarGananciaMenuPreelaborado(double ganancia)
        {
            PreElaborado.Ganancia = ganancia;
        }

        public bool ValidarData(string pProveedor, decimal pCosto, string pDescripcion)
        {
            return (pProveedor != "" && pCosto > 0 && pDescripcion != "");
        }

        public bool ValidarData(Chef pChef, List<IngredientesPorMenu> pIngredientes, double pGanancia, string pDesc)
        {
            return (pChef != null && 
                        pIngredientes != null && 
                            pIngredientes.Count > 0 && 
                                pGanancia > 0 && 
                                    pDesc != "");
        }
    }
}
