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
    public sealed class CMenu : ISerializable
    {
        #region Singleton
        private static CMenu _instancia = null;
        private static readonly object bloqueo = new Object();

        private CMenu() {
            _Menues = new List<Menu>();
        }

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

        public ExitCode AltaMenuPreelaborado(string pProveedor, decimal pCosto, string pDesc)
        {
            var exit = ExitCode.INPUT_DATA_ERROR;

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
                exit = ExitCode.OK;
            }

            return exit;
        }

        public ExitCode AltaMenuPropio(Chef pChef, List<IngredientesPorMenu> pIngredientes, decimal pGanancia, string pDesc)
        {
            var exit = ExitCode.INPUT_DATA_ERROR;

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

                exit = ExitCode.OK;
            }

            return exit;
        }

        public Menu Buscar(int pIdMenu)
        {
            Menu m = null;
            bool encontrado = false;
            int contador = 0;
            while (!encontrado && contador < _Menues.Count)
            {
                if (_Menues[contador].Id == pIdMenu)
                    m = _Menues[contador];

                contador++;
            }

            return m;
        }

        public Menu BuscarActivo(int pIdMenu)
        {
            Menu m = Buscar(pIdMenu);
            if (!m.Activo)
                m = null;

            return m;
        }

        public List<Menu> ListarMenuesActivos()
        {
            List<Menu> ret = new List<Menu>();
            foreach (Menu m in _Menues)
                if (m.Activo)
                    ret.Add(m);

            return ret;
        }

        public List<Menu> ListadoMenuesPorChef(Chef pChef)
        {
            List<Menu> ret = new List<Menu>();
            foreach (Menu m in ListarMenuesActivos())
                if (m is Propio && ((Propio)m).Chef.Equals(pChef))
                    ret.Add(m);

            return ret;
        }

        public ExitCode ModificarIngredientesDeMenu(int pIdMenu, IngredientesPorMenu pIngrediente)
        {
            var exit = ExitCode.NO_MENU_ERROR;
            Menu m = BuscarActivo(pIdMenu);

            if (m != null && m is Propio)
            {
                if (((Propio)m).TieneIngrediente(pIngrediente.Ingrediente))
                    ((Propio)m).ActualizarIngrediente(pIngrediente.Ingrediente, pIngrediente.Cantidad);
                else
                    ((Propio)m).Ingredientes.Add(pIngrediente);

                m.PrecioVenta = m.CalcularPrecioVenta();
                exit = ExitCode.OK;
            }

            return exit;
        }

        public List<IngredientesPorMenu> ListadoIngredientesPorMenu(int pIdMenu)
        {
            List<IngredientesPorMenu> ret = new List<IngredientesPorMenu>();
            Propio p = (Propio)Buscar(pIdMenu);

            if (p != null)
                ret = p.Ingredientes;

            return ret;
        }

        public List<Menu> ListadoMenuesConIngrediente(Ingrediente i)
        {
            List<Menu> ret = new List<Menu>();
            foreach (Menu m in _Menues)
                if (m is Propio && ((Propio)m).TieneIngrediente(i))
                    ret.Add(m);

            return ret;
        }

        public void CargarGananciaMenuPreelaborado(decimal ganancia)
        {
            PreElaborado.Ganancia = ganancia;
        }

        public bool ValidarData(string pProveedor, decimal pCosto, string pDescripcion)
        {
            return (pProveedor != "" && pCosto > 0 && pDescripcion != "");
        }

        public bool ValidarData(Chef pChef, List<IngredientesPorMenu> pIngredientes, decimal pGanancia, string pDesc)
        {
            return (pChef != null && 
                        pIngredientes != null && 
                            pIngredientes.Count > 0 && 
                                pGanancia > 0 && 
                                    pDesc != "");
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("listaMenues", this._Menues, typeof (List<Menu>));
        }

        public CMenu (SerializationInfo info, StreamingContext context)
        {
            this._Menues = info.GetValue("listaMenues", typeof(List<Menu>)) as List<Menu>;
            CMenu._instancia = this;
        }
    }
}
