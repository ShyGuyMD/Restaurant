using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Controladoras;
using Dominio.Clases;
using System.IO;

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
        public bool AltaMenuPreelaborado(string pProveedor, decimal pCosto, string pDesc)
        {
            return CMenu.Get.AltaMenuPreelaborado(pProveedor, pCosto, pDesc);
        }
        
        // Ver alternativas al diccionario.
        //public bool AltaMenuPropio(int pIdChef, Dictionary<int, int> pIngredientes, double pGanancia, string pDesc)
        //pIngredientes y pCantidades deberían tener igual tamaño, y corresponderse en sus posiciones entre sí.
        public bool AltaMenuPropio(string pChefDoc, string pChefTipoDoc, List<string> pIngredientes, List<int> pCantidades, double pGanancia, string pDesc)
        {
            bool ret = false;

            if (pIngredientes.Count == pCantidades.Count)
            {
                Documento d = CDocumento.Get.ArmarDocumento(pChefDoc, pChefTipoDoc);
                Chef c = CUsuario.Get.Buscar(d);

                List<IngredientesPorMenu> ingredientes = new List<IngredientesPorMenu>();
                int contador = 0;
                bool hayError = false;

                while (contador < pIngredientes.Count && !hayError)
                {
                    Ingrediente i = CIngrediente.Get.Buscar(pIngredientes[contador]);
                    if (i == null || !i.Activo)
                        hayError = true;
                    else
                    {
                        IngredientesPorMenu ipm = CIngrediente.Get.ArmarObjetoIngrediente(i, pCantidades[contador]);
                        ingredientes.Add(ipm);
                    }

                    contador++;
                }

                ret = CMenu.Get.AltaMenuPropio(c, ingredientes, pGanancia, pDesc);
            }

            return ret;
        }

        public List<Menu> ListadoMenuesConPrecio()
        {
            return CMenu.Get.ListarMenues();
        }
        
        // Usar Diccionario en caso de querer hacer las cosas bien.
        public bool ModificarMenu(int pIdMenu, List<string> pIngredientes, List<int> pCantidades)
        {
            return CMenu.Get.ModificarMenu(new Propio(), new List<IngredientesPorMenu>());
        }

        public List<Menu> ListadoMenuesConIngrediente(int pIdIngrediente)
        {
            return CMenu.Get.ListadoMenuesConIngrediente(pIdIngrediente);
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

        public int BuscarUsuario(string pLogin)
        {
            return 0;
        }
        public int BuscarRol(int a)
        {
            return 0;
        }
        #endregion

        #region Reserva
        public bool AltaReserva(string pNombrePersona, int pCantPersonas, DateTime pFechaReserva, List<int> pIdMenues, int pNumeroMesa)
        {
            bool ret = false, existenTodos = true;
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

            if (listaMenues.Count > 0 && existenTodos && mesa != null)
                ret = CReserva.Get.Alta(pNombrePersona, pCantPersonas, pFechaReserva, listaMenues, mesa);

            return ret;
        }

        public bool BajaReserva(string pNombre, DateTime pFechaReserva, int pIdMesa)
        {
            return CReserva.Get.Baja(pNombre, pFechaReserva, pIdMesa);
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            return CReserva.Get.ListadoReservasPorFecha(pFechaReserva);
        }
        #endregion

        #region Mesa
        public bool AltaMesa(int pNumero, int pCapacidad, string pUbicacion)
        {
            return CMesa.Get.Alta(pNumero, pCapacidad, pUbicacion);
        }
        #endregion

        #region Otros
        public void ActualizarParametros(double pGanancia)
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
                    double ganancia = Convert.ToDouble(data[1]);

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
