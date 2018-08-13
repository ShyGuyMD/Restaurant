using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using Aplicacion;
using Dominio.Controladoras;
using Dominio.Clases;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Encryption e = new Encryption();
            Fachada.Get.AltaChef("chef1", e.EncryptToString("chef"), "Chef", "111111", "Cedula", "Master", "Chef", 10000);
            Fachada.Get.AltaIngrediente("A001", "Papas", 25);

            List<string> ingredientes = new List<string>() { "A001" };
            List<int> cantidades = new List<int>() { 8 };

            Fachada.Get.AltaMenuPropio("111111", "Cedula", ingredientes, cantidades, 20, "Menu de Ejemplo");

            Fachada.Get.AltaIngrediente("A002", "Huevo", 30);
            ingredientes[0] = "A002";
            cantidades[0] = 3;

            Fachada.Get.ModificarIngredientesDeMenu(1, ingredientes, cantidades);
            
            
            /*
            Encryption e = new Encryption();

            Console.WriteLine("Insertando Administradores...\n");
            Console.ReadKey();
            Console.WriteLine("Admin 1: " + Fachada.Get.AltaAdmin("admin1", e.EncryptToString("admin"), "Administrador"));
            Console.WriteLine("Admin 2: " + Fachada.Get.AltaAdmin("admin2", e.EncryptToString("admin"), "Administrador"));
            Console.WriteLine("##################################################\n");

            Console.WriteLine("Insertando Chefs...\n");
            Console.ReadKey();
            Console.WriteLine("Chef 1: " + Fachada.Get.AltaChef("chef1", e.EncryptToString("chef"), "Chef", "111111", "Cedula", "Master", "Chef", 10000));
            Console.WriteLine("Chef 2: " + Fachada.Get.AltaChef("chef2", e.EncryptToString("chef"), "Chef", "222222", "Pasaporte", "Juan", "Lavatupper", 20000));
            Console.WriteLine("Chef 3: " + Fachada.Get.AltaChef("chef3", e.EncryptToString("chef"), "Chef", "333333", "Otros", "María", "Perez", 30000));
            Console.WriteLine("##################################################\n");

            Console.WriteLine("Listado de Todos los Usuarios\n");
            Console.ReadKey();
            foreach (Usuario u in CUsuario.Get._Usuarios)
            {
                Console.WriteLine(u.ToString());
                if (u != CUsuario.Get._Usuarios.Last<Usuario>())
                    Console.WriteLine("--------------------------------------------------------");
            }

            Console.WriteLine("##################################################\n");

            Console.WriteLine("Listado de Chefs\n");
            Console.ReadKey();
            foreach (Chef c in CUsuario.Get._Chef)
            {
                Console.WriteLine(c.ToString());
                if (c != CUsuario.Get._Chef.Last<Chef>())
                    Console.WriteLine("--------------------------------------------------------");
            }
            */
            Console.ReadKey();
        }
    }
}
