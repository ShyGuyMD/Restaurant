using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Controladoras;
using Dominio.Clases;
using System.IO;
using Helpers;
using static Helpers.Utils;

namespace Aplicacion
{
    public class Fachada
    {
        #region Singleton
        private static Fachada _instancia = null;
        private static readonly object bloqueo = new Object();
        private Fachada() { }

        public static Fachada Get
        {
            get
            {
                lock (bloqueo)
                {
                    if (_instancia == null)
                        _instancia = new Fachada();

                    return _instancia;
                }
            }
        }
        #endregion

        #region Menu
        public ExitCode AltaMenuPreelaborado(string pProveedor, decimal pCosto, string pDesc)
        {
            return CMenu.Get.AltaMenuPreelaborado(pProveedor, pCosto, pDesc);
        }
        
        //pIngredientes y pCantidades deberían tener igual tamaño, y corresponderse en sus posiciones entre sí.
        public ExitCode AltaMenuPropio(string pChefDoc, string pChefTipoDoc, List<string> pIngredientes, List<int> pCantidades, decimal pGanancia, string pDesc)
        {
            ExitCode ret = ExitCode.PLACEHOLDER;

            if (pIngredientes.Count == pCantidades.Count)
            {
                Documento d = CDocumento.Get.ArmarDocumento(pChefDoc, pChefTipoDoc);
                Chef c = CUsuario.Get.BuscarChef(d);

                if (c != null) { 
                    List<IngredientesPorMenu> ingredientes = new List<IngredientesPorMenu>();
                    int contador = 0;
                    bool hayError = false;

                    while (contador < pIngredientes.Count && !hayError)
                    {
                        Ingrediente i = CIngrediente.Get.BuscarActivo(pIngredientes[contador]);
                        if (i == null)
                        {
                            hayError = true;
                            ret = ExitCode.NO_INGREDIENT_ERROR;
                        }
                        else
                        {
                            IngredientesPorMenu ipm = CIngrediente.Get.ArmarObjetoIngrediente(i, pCantidades[contador]);
                            ingredientes.Add(ipm);
                        }

                        contador++;
                    }

                    if (!hayError)
                        ret = CMenu.Get.AltaMenuPropio(c, ingredientes, pGanancia, pDesc);
                }
                else ret = ExitCode.NO_CHEF_ERROR;
            }
            else ret = ExitCode.INPUT_DATA_ERROR;

            return ret;
        }

        public List<Menu> ListadoMenuesConPrecio()
        {
            return CMenu.Get.ListarMenuesActivos();
        }
        
        public ExitCode ModificarIngredientesDeMenu(int pIdMenu, List<string> pIngredientes, List<int> pCantidades)
        {
            var exit = ExitCode.PLACEHOLDER;

            List<IngredientesPorMenu> ingredientes = new List<IngredientesPorMenu>();
            int contador = 0;
            while (contador < pIngredientes.Count)
            {
                Ingrediente i = CIngrediente.Get.BuscarActivo(pIngredientes[contador]);

                if (i != null)
                {
                    IngredientesPorMenu ing = CIngrediente.Get.ArmarObjetoIngrediente(i, pCantidades[contador]);
                    ingredientes.Add(ing);
                }
                else
                    exit = ExitCode.NO_INGREDIENT_ERROR;

                contador++;
            }

            if (exit != ExitCode.NO_INGREDIENT_ERROR)
                exit = CMenu.Get.ModificarIngredientesDeMenu(pIdMenu, ingredientes);

            return exit;
        }

        // ACA
        public List<Menu> ListadoMenuesConIngrediente(string pCodigo)
        {
            List<Menu> ret = null;
            Ingrediente i = CIngrediente.Get.BuscarActivo(pCodigo);

            if (i != null)
                ret = CMenu.Get.ListadoMenuesConIngrediente(i);

            return ret;
        }

        public List<IngredientesPorMenu> ListadoIngredientesPorMenu(int pIdMenu)
        {
            return CMenu.Get.ListadoIngredientesPorMenu(pIdMenu);
        }
        #endregion

        #region Ingrediente
        public bool AltaIngrediente(string pCodigo, string pDescripcion, decimal pCosto)
        {
            return CIngrediente.Get.AltaIngrediente(pCodigo, pDescripcion, pCosto);
        }
        public bool BajaIngrediente(string pCodigo)
        {
            return CIngrediente.Get.BajaIngrediente(pCodigo);
        }
        #endregion

        #region Chef / Usuario
        public bool AltaAdmin(string pUsername, string pPassword, string pRol)
        {
            Usuario.Rol rolAsociado = CUsuario.Get.RolAsociado(pRol);

            return CUsuario.Get.AltaAdmin(pUsername, pPassword, rolAsociado);
        }
        public bool AltaChef(string pUsername, string pPassword, string pRol, string pNumDoc, string pTipoDoc, string pNombre, string pApellido, decimal pSueldo)
        {
            Documento documento = CDocumento.Get.ArmarDocumento(pNumDoc, pTipoDoc);
            Usuario.Rol rolAsociado = CUsuario.Get.RolAsociado(pRol);
            
            return CUsuario.Get.AltaChef(pUsername, pPassword, rolAsociado, documento, pNombre, pApellido, pSueldo);
        }
        public bool Login(string pUsername, string pPassword)
        {
            return CUsuario.Get.Login(pUsername, pPassword);
        }

        // ??????????????????????
        public int BuscarUsuario(string pLogin)
        {
            return 0;
        }
        public int BuscarRol(int a)
        {
            return 0;
        }
        // ??????????????????????
        #endregion

        #region Reserva
        public string AltaReserva(string pNombrePersona, int pCantPersonas, DateTime pFechaReserva, List<int> pIdMenues, int pNumeroMesa)
        {
            bool existenTodos = true;
            string ret = "";
            int contador = 0;
            List<Menu> listaMenues = new List<Menu>();
            
            while (existenTodos && contador < pIdMenues.Count)
            {
                Menu m = CMenu.Get.Buscar(pIdMenues[contador]);
                if (m != null)
                    listaMenues.Add(m);
                else
                    existenTodos = false;

                contador++;
            }
            Mesa mesa = CMesa.Get.Buscar(pNumeroMesa);

            if (listaMenues.Count > 0 && existenTodos && mesa != null) // ACOMODAR ESTO
                ret = CReserva.Get.Alta(pNombrePersona, pCantPersonas, pFechaReserva, listaMenues, mesa);

            return ret;
        }

        public bool BajaReserva(string pCodReserva)
        {
            return CReserva.Get.Baja(pCodReserva);
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            return CReserva.Get.ListadoReservasPorFecha(pFechaReserva);
        }

        public Reserva BuscarReservaPorCodigo(string pCodReserva)
        {
            return CReserva.Get.BuscarActivo(pCodReserva);
        }
        #endregion

        #region Mesa
        public bool AltaMesa(int pNumero, int pCapacidad, string pUbicacion)
        {
            return CMesa.Get.Alta(pNumero, pCapacidad, pUbicacion);
        }

        public bool ListadoMesas()
        {
            return false;
        }
        #endregion

        #region Otros
        public void ActualizarParametros(decimal pGanancia)
        {
            throw new NotImplementedException();
        }
        public void CargarParametros(string filepath)
        {
            StreamReader sr = null;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            string fileLine = "";

            try
            {
                sr = new StreamReader(fs);
                while ((fileLine = sr.ReadLine()) != null)
                {
                    string[] data = fileLine.Split(':');
                    decimal ganancia = Convert.ToDecimal(data[1]);

                    CMenu.Get.CargarGananciaMenuPreelaborado(ganancia);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        public void CargarIngredientesDeArchivo(string filepath)
        {
            StreamReader sr = null;
            FileStream fs = new FileStream(filepath, FileMode.Open);
            string fileLine = "";

            try
            {
                sr = new StreamReader(fs);
                while ((fileLine = sr.ReadLine()) != null)
                {
                    string[] data = fileLine.Split('@');
                    string codigo = data[0];
                    string descripcion = data[1];
                    decimal costo = Convert.ToDecimal(data[2]);

                    Fachada.Get.AltaIngrediente(codigo, descripcion, costo);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        public void CargarDatosDePrueba()
        {

        }
        public bool HayDatos()
        {
            return CMenu.Get._Menues.Count > 0 ||
                   CIngrediente.Get._Ingredientes.Count > 0 ||
                   CReserva.Get._Reservas.Count > 0;
        }
        #endregion
    }
}
