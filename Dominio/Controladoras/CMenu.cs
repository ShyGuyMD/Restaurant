﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Clases;
using static Helpers.Utils;

namespace Dominio.Controladoras
{
    public sealed class CMenu
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
            ExitCode exit = ExitCode.PLACEHOLDER;

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
            else
            {
                exit = ExitCode.INPUT_DATA_ERROR;
            }

            return exit;
        }

        public ExitCode AltaMenuPropio(Chef pChef, List<IngredientesPorMenu> pIngredientes, double pGanancia, string pDesc)
        {
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

                return ExitCode.OK;
            }

            return ExitCode.INPUT_DATA_ERROR;
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

        public List<Menu> ListarMenuesActivos()
        {
            List<Menu> ret = new List<Menu>();
            foreach (Menu m in _Menues)
                if (m.Activo)
                    ret.Add(m);

            return ret;
        }

        public bool ModificarIngredientesDeMenu(int pIdMenu, List<IngredientesPorMenu> pIngredientes)
        {
            return false;
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
            return null;
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
