using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;

namespace Restaurante
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            // Buscar en lista de usuarios.

            //hacer en fachada las funciones que faltan. Deben devolver strings.

            Session["Usuario"] = null;
            Session["Rol"] = null;

            //si el usuario existe
            if (Fachada.Get.BuscarUsuario(Login1.UserName) != null)
            {
                if (Fachada.Get.Login(Login1.UserName, Login1.Password))
                {
                    Session["Usuario"] = Fachada.Get.BuscarUsuario(Login1.UserName);
                    Session["Rol"] = Fachada.Get.BuscarRol((int)Session["Usuario"]);
                    e.Authenticated = true;

                }
                else
                {
                    Session["Usuario"] = null;
                    e.Authenticated = false;
                    //TO DO: Tirar enum de errores.
                    Response.Write("Contraseña Incorrecta.");
                }
            }
            else
            {
                Session["Usuario"] = null;
                e.Authenticated = false;
                //TO DO: Tirar enum de errores.
                Response.Write("Nombre de usuario incorrecto.");
            }
        }
    }
}