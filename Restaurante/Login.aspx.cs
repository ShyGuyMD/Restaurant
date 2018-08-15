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

            Session["Usuario"] = null;
            Session["Rol"] = null;

            Encryption enc = new Encryption();
            
            if (Fachada.Get.Login(LoginRestaurant.UserName, enc.EncryptToString(LoginRestaurant.Password)) != Utils.ExitCode.OK)
            {
                Session["Usuario"] = LoginRestaurant.UserName;
                Session["Rol"] = Fachada.Get.RolPorUsuario((string)Session["Usuario"]);
                e.Authenticated = true;

            }
            else
            {
                Session["Usuario"] = null;
                Session["Rol"] = null;
                e.Authenticated = false;
                Response.Write("Datos Incorrectos");
            }
        }
    }
}