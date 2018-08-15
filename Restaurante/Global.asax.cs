using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using Aplicacion;

namespace Restaurante
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                string ingredientes = HttpRuntime.AppDomainAppPath + @"config\ingredientes.txt";
                string parametros = HttpRuntime.AppDomainAppPath + @"config\parametros.txt";
                string rutaSerializacion = HttpRuntime.AppDomainAppPath + @"config\serial.bin";

                if (File.Exists(rutaSerializacion))
                {
                    Repositorio rep = new Repositorio(rutaSerializacion);
                    rep.Deserialize();
                }
                else
                {
                    Fachada.Get.CargarIngredientesDeArchivo(ingredientes);
                    Fachada.Get.CargarDatosDePrueba();
                }

                if (File.Exists(parametros))
                {
                    Fachada.Get.CargarParametros(parametros);
                }
            }
            catch
            {
                throw;
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Serializar todo
            string rutaSerializacion = HttpRuntime.AppDomainAppPath + @"config\serial.bin";
            Repositorio rep = new Repositorio(rutaSerializacion);
            rep.Serialize();
        }
    }
}