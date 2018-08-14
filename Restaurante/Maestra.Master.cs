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
            int a = 0;
            //switch (Session["Rol"])
            switch (a)
            {
                case 0:
                    MenuAdmin.Visible = true;
                    break;

                case 1:
                    MenuChef.Visible = true;
                    break;

                default:
                    MenuChef.Visible = false;
                    MenuAdmin.Visible = false;
                    break;
            }
            

        }

        public bool VerificarUsuario(int pUser)
        {
            return (int)Session["Rol"] == pUser;
        }

        public void LogOut()
        {
            Session["Usuario"] = null;
            Session["Rol"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}