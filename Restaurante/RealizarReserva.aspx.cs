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

        private List<int> idMenues = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        protected void btnReservar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int personas = int.Parse(txtPersonas.Text);
            DateTime fecha = calFecha.SelectedDate;
            fecha.AddHours(double.Parse(txtHoras.Text));
            fecha.AddHours(double.Parse(txtHoras.Text));
            int mesa = int.Parse(lblMesaData.Text);


            if (ValidarDatos(nombre, personas, fecha, mesa))
            {
                string resultado = Fachada.Get.AltaReserva(nombre, personas, fecha, idMenues, mesa);

                if (resultado != "")
                {
                    Response.Write("Reserva realizada con éxito. Su código es: " + resultado);
                    idMenues.Clear();
                }
                else
                {
                    Response.Write("Reserva fallida. Espere e inténtelo nuevamente.");
                }
            }
        }

        protected void CargarDatos()
        {

            lstMenu.DataTextField = "Datos";
            lstMenu.DataValueField = "Id";
            lstMenu.DataSource = Fachada.Get.ListadoMenuesConPrecio();
            lstMenu.DataBind();
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
            ((List<int>)Session["menues"]).Add(int.Parse(lstMenu.SelectedValue));
        }

        protected bool ValidarDatos(string pNombre, int pPersonas, DateTime pFecha, int pIdMesa)
        {
            return (pNombre != "" && pPersonas > 0 && pPersonas > 0 && pFecha > DateTime.Now && pIdMesa > 0);
        }
    }

}