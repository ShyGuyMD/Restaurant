using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Aplicacion;

namespace Restaurante
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Primero revisar si hay datos serializados.
            //if (!Fachada.Get.HayDatos())
            //{
            //    Fachada.Get.CargarIngredientesDeArchivo(HttpRuntime.AppDomainAppPath + @"Ingredientes.txt");
            //    Fachada.Get.CargarDatosDePrueba();
            //}
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Serializar todo
            string rutaSerializacion = HttpRuntime.AppDomainAppPath + @"Config\serial.bin";\
            Repositorio rep = new Repositorio(rutaSerializacion);
            rep.Serializable();
        }
    }
}