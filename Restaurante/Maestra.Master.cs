using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;
using static Helpers.Utils;

namespace Restaurante
{
    public partial class Maestra : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Session["Rol"])
            {
                case 0:
                    MenuAdmin.Visible = true;
                    MenuChef.Visible = false;
                    break;

                case 1:
                    MenuChef.Visible = true;
                    MenuAdmin.Visible = false;
                    break;

                default:
                    MenuChef.Visible = false;
                    MenuAdmin.Visible = false;
                    break;
            }
            

        }

        public bool VerificarUsuario(string pUser)
        {
            return (string)Session["Rol"] == pUser;
        }

        public void LogOut()
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        public static string MensajeError(int pExitCode, string pFuncion)
        {
            string mensaje = "";
            switch (pExitCode)
            {
                case 0:
                    mensaje = "OK";
                    break;
                case 1:
                    mensaje = "Hay campos requeridos sin completar";
                    break;
                case 2:
                    mensaje = "Ingrediente no encontrado";
                    break;
                case 3:
                    mensaje = "Chef no encontrado";
                    break;
                case 4:
                    mensaje = "Menu no encontrado";
                    break;
                case 5:
                    mensaje = "Codigo de reserva invalido";
                    break;
                case 6:
                    mensaje = "Ya existe la mesa que esta tratando de ingresar";
                    break;
                case 7:
                    mensaje = "Ya existe el usuario que esta tratando de ingresar";
                    break;
                case 8:
                    mensaje = "Ya existe el ingrediente que esta tratando de ingresar";
                    break;
                case 9:
                    mensaje = "Nombre de usuario/contraseña incorrecto";
                    break;

                default:
                    mensaje = "Ha ocurrido un error inesperado";
                    break;
            }

            return mensaje;
        }
    }
}