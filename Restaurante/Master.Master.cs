using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;

namespace Restaurante
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            switch (Session["Rol"])
            {
                case 0:
                    //mostrar menu de admin
                    break;

                case 1:
                    //mostrar menu de chef
                    break;

                default:
                    break;
            }


            //if user == null, rol es incorrecto 
            //Logout

        }
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (Session["Usuario"] == null || (Session["Usuario"] as CUsuario).Rol != "Asistente")
        //    {
        //        LogOut();
        //    }
        //}

        //protected void LogOut()
        //{
        //    Session["Usuario"] = null;
        //    Response.Redirect("Login.aspx");
        //}

        //protected void btnLogout_Click(object sender, EventArgs e)
        //{
        //    LogOut();
        //}
    }
}