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

        public List<Menu> ListadoMenuesPorChef(string pUsername)
        {
            Usuario u = CUsuario.Get.BuscarUsuario(pUsername);

            return CMenu.Get.ListadoMenuesPorChef((Chef)u);
        }
        
        public ExitCode ModificarIngredientesDeMenu(int pIdMenu, string pCodIngrediente, int pCantIngrediente)
        {
            var exit = ExitCode.PLACEHOLDER;
            Ingrediente ingrediente = CIngrediente.Get.BuscarActivo(pCodIngrediente);
            IngredientesPorMenu ipm = null;
            if (ingrediente != null)
                ipm = CIngrediente.Get.ArmarObjetoIngrediente(ingrediente, pCantIngrediente);
            else
                exit = ExitCode.NO_INGREDIENT_ERROR;

            if (exit != ExitCode.NO_INGREDIENT_ERROR)
                exit = CMenu.Get.ModificarIngredientesDeMenu(pIdMenu, ipm);

            return exit;
        }

        public List<Menu> ListadoMenuesConIngrediente(string pCodigo)
        {
            List<Menu> ret = new List<Menu>();
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
        public ExitCode AltaIngrediente(string pCodigo, string pDescripcion, decimal pCosto)
        {
            return CIngrediente.Get.AltaIngrediente(pCodigo, pDescripcion, pCosto);
        }
        public ExitCode BajaIngrediente(string pCodigo)
        {
            return CIngrediente.Get.BajaIngrediente(pCodigo);
        }
        #endregion

        #region Chef / Usuario
        public ExitCode AltaAdmin(string pUsername, string pPassword, string pRol)
        {
            Usuario.Rol rolAsociado = CUsuario.Get.RolAsociado(pRol);

            return CUsuario.Get.AltaAdmin(pUsername, pPassword, rolAsociado);
        }

        public ExitCode AltaChef(string pUsername, string pPassword, string pRol, string pNumDoc, string pTipoDoc, string pNombre, string pApellido, decimal pSueldo)
        {
            Documento documento = CDocumento.Get.ArmarDocumento(pNumDoc, pTipoDoc);
            Usuario.Rol rolAsociado = CUsuario.Get.RolAsociado(pRol);
            
            return CUsuario.Get.AltaChef(pUsername, pPassword, rolAsociado, documento, pNombre, pApellido, pSueldo);
        }

        public string RolPorUsuario(string pUsername)
        {
            return CUsuario.Get.RolPorUsuario(pUsername);
        }

        public ExitCode Login(string pUsername, string pPassword)
        {
            return CUsuario.Get.Login(pUsername, pPassword);
        }

        #endregion

        #region Reserva
        public string AltaReserva(string pNombrePersona, int pCantPersonas, DateTime pFechaReserva, List<int> pIdMenues, int pNumeroMesa)
        {
            bool existenTodos = true;
            string ret = "";
            int contador = 0;
            List<Menu> listaMenues = new List<Menu>();

            if (pIdMenues != null)
            {
                while (existenTodos && contador < pIdMenues.Count)
                {
                    Menu m = CMenu.Get.Buscar(pIdMenues[contador]);
                    if (m != null)
                        listaMenues.Add(m);
                    else
                        existenTodos = false;

                    contador++;
                }
            }
            Mesa mesa = CMesa.Get.Buscar(pNumeroMesa);

            if (existenTodos && mesa != null)
                ret = CReserva.Get.Alta(pNombrePersona, pCantPersonas, pFechaReserva, listaMenues, mesa);

            return ret;
        }

        public ExitCode BajaReserva(string pCodReserva)
        {
            return CReserva.Get.Baja(pCodReserva);
        }

        public List<Reserva> ListadoReservasPorFecha(DateTime pFechaReserva)
        {
            return CReserva.Get.ListadoReservasPorFecha(pFechaReserva);
        }

        public Reserva BuscarReservaPorCodigo(string pCodReserva)
        {
            return CReserva.Get.BuscarPorCodigo(pCodReserva);
        }
        #endregion

        #region Mesa
        public ExitCode AltaMesa(int pNumero, int pCapacidad, string pUbicacion)
        {
            return CMesa.Get.Alta(pNumero, pCapacidad, pUbicacion);
        }

        public Mesa BuscarMesaDisponible(int pCantidadPersonas, DateTime pFechaReserva)
        {
            List<Mesa> reservadas = new List<Mesa>();
            foreach (Reserva r in ListadoReservasPorFecha(pFechaReserva))
            {
                reservadas.Add(r.Mesa);
            }
            return CMesa.Get.BuscarDisponible(pCantidadPersonas, reservadas);
        }

        public List<Mesa> ListadoMesasDisponibles(int pCapacidad)
        {
            return CMesa.Get.ListarDisponibles(pCapacidad);
        }
        #endregion

        #region Otros
        public void ActualizarParametros(string filepath)
        {
            StreamWriter sw = null;
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);

            try
            {
                sw = new StreamWriter(fs);
                sw.WriteLine("GananciaPreElaborados:" + PreElaborado.Ganancia.ToString());
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
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

                    AltaIngrediente(codigo, descripcion, costo);
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
            Encryption e = new Encryption();
            
            //AltaAdmin(string pUsername, string pPassword, string pRol)
            AltaAdmin("admin", e.EncryptToString("admin"), "Administrador");
            
            //AltaChef(string pUsername, string pPassword, string pRol, string pNumDoc, string pTipoDoc, string pNombre, string pApellido, decimal pSueldo)
            AltaChef("angel@mail.com", e.EncryptToString("angel"), "Chef", "1.111.111-1", "Cedula", "Warren", "Worthington", 30000);
            AltaChef("beast@mail.com", e.EncryptToString("beast"), "Chef", "2.222.222-2", "Pasaporte", "Hank", "McCoy", 25000);
            AltaChef("cyclops@mail.com", e.EncryptToString("cyclops"), "Chef", "3.333.333-3", "Otros", "Scott", "Summers", 10000);
            AltaChef("iceman@mail.com", e.EncryptToString("iceman"), "Chef", "4.444.444-4", "Cedula", "Bobby", "Drake", 40000);
            AltaChef("marvelgirl@mail.com", e.EncryptToString("marvelgirl"), "Chef", "5.555.555-5", "Pasaporte", "Jean", "Grey", 50000);
            AltaChef("professorx@mail.com", e.EncryptToString("professorx"), "Chef", "6.666.666-6", "Otros", "Charles", "Xavier", 35000);
            AltaChef("magneto@mail.com", e.EncryptToString("magneto"), "Chef", "7.777.777-7", "Cedula", "Max", "Eisenhardt", 15000);
            AltaChef("storm@mail.com", e.EncryptToString("storm"), "Chef", "8.888.888-8", "Pasaporte", "Ororo", "Munroe", 35000);
            AltaChef("colossus@mail.com", e.EncryptToString("colossus"), "Chef", "9.999.999-9", "Otros", "Piotr", "Rasputin", 45000);
            
            //AltaMesa(int pNumero, int pCapacidad, string pUbicacion)
            AltaMesa(1, 2, "Pareja - Ala Norte");
            AltaMesa(2, 5, "Familiar - Ala Norte");
            AltaMesa(3, 12, "Reunion - Ala Norte");
            AltaMesa(4, 2, "Pareja - Ala Este");
            AltaMesa(5, 5, "Familiar - Ala Este");
            AltaMesa(6, 12, "Reunion - Ala Este");
            AltaMesa(7, 2, "Pareja - Ala Sur");
            AltaMesa(8, 5, "Familiar - Ala Sur");
            AltaMesa(9, 12, "Reunion - Ala Sur");
            AltaMesa(10, 2, "Pareja - Ala Oeste");
            AltaMesa(11, 5, "Familiar - Ala Oeste");
            AltaMesa(12, 12, "Reunion - Ala Oeste");
            
            //AltaMenuPreelaborado(string pProveedor, decimal pCosto, string pDesc)
            AltaMenuPreelaborado("Mc Dudels", 120, "Hamburguesa con queso");
            AltaMenuPreelaborado("Mc Dudels", 220, "Hamburguesa doble con queso");
            AltaMenuPreelaborado("Pizza Queen", 300, "Pizzeta familiar");
            AltaMenuPreelaborado("Pizza Queen", 400, "Pizzeta familiar con gustos");
            AltaMenuPreelaborado("Mc Dudels", 85, "Papas fritas");

            // Los Ingredientes de Código A01 hasta A43 se cargan desde el archivo de texto ingredientes.txt
            //AltaMenuPropio(string pChefDoc, string pChefTipoDoc, List<string> pIngredientes, List<int> pCantidades, decimal pGanancia, string pDesc)
            AltaMenuPropio("1.111.111-1", "Cedula", new List<string> { "A42", "A33" }, new List<int> { 6, 2 }, 15, "Welfare Particle Prosper");
            AltaMenuPropio("1.111.111-1", "Cedula", new List<string> { "A17", "A41", "A18" }, new List<int> { 4, 2, 8 }, 15, "Sharp Offer Jacket");
            AltaMenuPropio("2.222.222-2", "Pasaporte", new List<string> { "A17" }, new List<int> { 3 }, 20, "Parallel Overall Laboratory");
            AltaMenuPropio("2.222.222-2", "Pasaporte", new List<string> { "A43" }, new List<int> { 9 }, 20, "Domination Reduction Think");
            AltaMenuPropio("3.333.333-3", "Otros", new List<string> { "A02", "A10" }, new List<int> { 1, 8 }, 10, "Fog Garbage Pension");
            AltaMenuPropio("3.333.333-3", "Otros", new List<string> { "A25", "A04", "A33", "A11", "A42" }, new List<int> { 6, 1, 6, 3, 4 }, 10, "Single Delay Arrogant");
            AltaMenuPropio("4.444.444-4", "Cedula", new List<string> { "A02", "A13" }, new List<int> { 10, 5 }, 15, "Parameter Sheep Unaware");
            AltaMenuPropio("4.444.444-4", "Cedula", new List<string> { "A28", "A07", "A15", "A39", "A30" }, new List<int> { 6, 5, 9, 10, 1 }, 15, "Salon Read Voter");
            AltaMenuPropio("5.555.555-5", "Pasaporte", new List<string> { "A24", "A07", "A10", "A16" }, new List<int> { 9, 4, 5, 10 }, 30, "Nonsense Disappear Speech");
            AltaMenuPropio("5.555.555-5", "Pasaporte", new List<string> { "A23" }, new List<int> { 5 }, 30, "Powder Way Bronze");
            AltaMenuPropio("6.666.666-6", "Otros", new List<string> { "A18", "A39", "A41", "A20", "A11" }, new List<int> { 4, 7, 2, 3, 8 }, 17.5m, "Rocket Battery Utter");
            AltaMenuPropio("6.666.666-6", "Otros", new List<string> { "A04" }, new List<int> { 5 }, 17.5m, "Traffic Elapse Offensive");
            AltaMenuPropio("7.777.777-7", "Cedula", new List<string> { "A15", "A36" }, new List<int> { 10, 1 }, 5, "Consumer Failure Counter");
            AltaMenuPropio("7.777.777-7", "Cedula", new List<string> { "A03" }, new List<int> { 1 }, 5, "Nerve Sit Clarify");
            AltaMenuPropio("8.888.888-8", "Pasaporte", new List<string> { "A21", "A23", "A36" }, new List<int> { 8, 7, 3 }, 22.5m, "Report Outline Shout");
            AltaMenuPropio("8.888.888-8", "Pasaporte", new List<string> { "A25", "A30", "A08", "A04" }, new List<int> { 10, 8, 6, 3 }, 22.5m, "Even Liberty Ceiling");
            AltaMenuPropio("9.999.999-9", "Otros", new List<string> { "A22", "A28", "A06" }, new List<int> { 2, 5, 4 }, 50, "Bait Barrier Black");
            AltaMenuPropio("9.999.999-9", "Otros", new List<string> { "A01", "A19", "A41", "A08" }, new List<int> { 10, 6, 6, 3 }, 50, "Slide Suntan Old Age");

            //public string AltaReserva(string pNombrePersona, int pCantPersonas, DateTime pFechaReserva, List<int> pIdMenues, int pNumeroMesa)
            AltaReserva("Anthony Stark", 6, new DateTime(2018, 10, 25), new List<int> { 16, 17, 9, 1, 10, 11 }, 3);
            AltaReserva("Peter Parker", 2, new DateTime(2018, 12, 24), null, 10);
            AltaReserva("Ash Ketchum", 3, new DateTime(2019, 01, 01), new List<int> { 12, 10 }, 8);
            AltaReserva("Bruce Wayne", 1, DateTime.Today.AddDays(1), null, 6);
            AltaReserva("Edward Cobblepot", 5, DateTime.Today.AddDays(1), new List<int> { 16, 17, 9 }, 5);
            AltaReserva("Percival Frederikstein Von Mussel Klossowski De Rolo III", 12, DateTime.Today.AddDays(1), new List<int> { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18 }, 6);
            AltaReserva("Guybrush Threepwood", 5, DateTime.Today.AddDays(1), new List<int> { 18 }, 11);
            AltaReserva("Daniel Rabinovich", 7, DateTime.Today.AddDays(1), new List<int> { 9, 1, 10, 11, 6 }, 3);
            AltaReserva("Esther Píscore", 3, DateTime.Today.AddDays(1), null, 8);
            AltaReserva("Don Rodrigo Diaz de Carreras", 2, DateTime.Today.AddDays(1), new List<int> { 9, 18 }, 10);
        }

        public bool HayDatos()
        {
            return CMenu.Get._Menues.Count > 0 ||
                   CIngrediente.Get._Ingredientes.Count > 0 ||
                   CReserva.Get._Reservas.Count > 0 ||
                   CMesa.Get._Mesas.Count > 0;
        }
        #endregion

    }
}
