using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;

namespace Restaurante
{
    public partial class Maestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Usuario"] != null)
            {
                MenuLogin.Visible = false;
                MenuLogout.Visible = true;
            }
            else
            {
                lblUsername.Text = (string)Session["Usuario"];
            }

            switch (Session["Rol"])
            {
                case 0:
                    MenuAdmin.Visible = true;
                    break;

                case 1:
                    MenuChef.Visible = true;
                    break;

                default:
                    MenuChef.Visible = false;
                    MenuChef.Visible = false;
                    break;
            }
            

        }

        public void VerificarUsuario(int pRol)
        {
            if (Session["Usuario"] == null || Fachada.Get.BuscarRol((int)Session["Usuario"]) != pRol)
            {
                LogOut();
            }
        }

        public void LogOut()
        {
            Session["Usuario"] = null;
            Session["Rol"] = null;

            MenuLogin.Visible = true;
            MenuLogout.Visible = false;
            Response.Redirect("Login.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            LogOut();
        }
    }
}