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
        public bool AltaMenuPropio(int pIdChef, Dictionary<int, int> pIngredientes, double pGanancia, string pDesc)
        {
            return CMenu.Get.AltaMenuPropio(pIdChef, pIngredientes, pGanancia, pDesc);
        }

        public List<Menu> ListadoMenuesConPrecio()
        {
            return CMenu.Get.ListarMenues();
        }
        // Usar Diccionario en caso de querer hacer las cosas bien.
        public bool ModificarMenu(int pIdMenu, int pIdIngrediente, int pCantidad)
        {
            return CMenu.Get.ModificarMenu(pIdMenu, pIdIngrediente, pCantidad);
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
        public bool BajaIngrediente(int pIdIngrediente)
        {
            return CIngrediente.Get.BajaIngrediente(pIdIngrediente);
        }
        #endregion

        #region Chef / Usuario
        public bool AltaChef(string pUsername, string pPassword, int pRol, string pNumDoc, int pTipoDoc, string pNombre, string pApellido, decimal pSueldo)
        {
            return CUsuario.Get.AltaChef(pUsername, pPassword, pRol, pNumDoc, pTipoDoc, pNombre, pApellido, pSueldo);
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
            return CReserva.Get.Alta(pNombrePersona, pCantPersonas, pFechaReserva, pIdMenues, pNumeroMesa);
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
