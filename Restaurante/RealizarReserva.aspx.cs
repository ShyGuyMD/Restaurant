using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;

namespace Restaurante
{
    public partial class RealizarReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
            List<Menu> lMenuProxy = new List<Menu>();
            Session["menues"] = lMenuProxy;
        }

        protected void btnReservar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int personas = int.Parse(txtPersonas.Text);
            DateTime fecha = calFecha.SelectedDate;
            fecha.AddHours(double. Parse(txtHoras.Text));
            fecha.AddHours(double.Parse(txtHoras.Text));
            int mesa = int.Parse(lstMesa.SelectedValue);
            List<Menu> menues = Session["menues"] as List<Menu>;
            
            

            if(ValidarDatos(nombre, personas, fecha, mesa))
            {
                string resultado = Fachada.Get.AltaReserva(nombre, personas, fecha, mesa);
                if (resultado != "")
                {
                    Response.Write("Reserva realizada con éxito. Su código es: " + resultado);
                }
                else
                {
                    Response.Write("Reserva fallida. Espere e inténtelo nuevamente.");
                }
            }
        }
        
        protected void CargarDatos()
        {
            lstMesa.DataTextField = "Datos";
            lstMesa.DataValueField = "Id";
            lstMesa.DataSource = Fachada.Get.ListadoMesas(pIdMenu);
            lstMesa.DataBind();

            lstMenu.DataTextField = "Datos";
            lstMenu.DataValueField = "Id";
            lstMenu.DataSource = Fachada.Get.ListadoMenues(pIdMenu);
            lstMenu.DataBind();
        }

        CargarMenus(List<Menu> pLista)
        {

        }


        protected void grillaMenus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = int.Parse(e.CommandArgument + "");
            int id = (int)grillaMenus.DataKeys[fila].Value;

            if (e.CommandName == "eliminar")
            {

            }
        }

        protected void BtnAgregarMenu_Click(object sender, EventArgs e)
        {

        }
    }
    }
}