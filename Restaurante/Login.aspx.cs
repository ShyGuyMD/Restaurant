using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;
using Helpers;

namespace Restaurante
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginRestaurant_Authenticate(object sender, AuthenticateEventArgs e)
        {
            // Buscar en lista de usuarios.

            //hacer en fachada las funciones que faltan. Deben devolver strings.

            Session["Usuario"] = null;
            Session["Rol"] = null;

            //si el usuario existe

            Encryption enc = new Encryption();
            
            if (Fachada.Get.Login(LoginRestaurant.UserName, enc.EncryptToString(LoginRestaurant.Password)))
            {
                Session["Usuario"] = Fachada.Get.BuscarUsuario(LoginRestaurant.UserName);
                Session["Rol"] = Fachada.Get.BuscarRol((int)Session["Usuario"]);
                e.Authenticated = true;

            }
            else
            {
                Session["Usuario"] = null;
                e.Authenticated = false;
                Response.Write("Datos Incorrectos");
            }
        }
    }
}