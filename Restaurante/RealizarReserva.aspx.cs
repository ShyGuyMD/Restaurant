using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;
using Dominio.Clases;

namespace Restaurante
{
    public partial class RealizarReserva : System.Web.UI.Page
    {




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
                PanelConfirmar.Visible = false;
                Session["idMenues"] = new List<int>();

            }
        }

        protected void btnReservar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            int personas = int.Parse(txtPersonas.Text);
            DateTime fecha = calFecha.SelectedDate;
            fecha = fecha.AddHours(double.Parse(txtHoras.Text));
            fecha = fecha.AddHours(double.Parse(txtHoras.Text));
            int mesa = int.Parse(lblMesaData.Text);


            if (ValidarDatos(nombre, personas, fecha, mesa))
            {
                string resultado = Fachada.Get.AltaReserva(nombre, personas, fecha, Session["idMenues"] as List<int>, mesa);

                if (resultado != "")
                {
                    Response.Write("Reserva realizada con éxito. Su código es: " + resultado + " para la mesa " + mesa);
                    (Session["idMenues"] as List<int>).Clear();
                    LimpiarTextos();
                    Session["idMenues"] = new List<int>();
                    grillaMenus.DataSource = null;
                    grillaMenus.DataBind();
                    lblMesaData.Text = "";
                }
                else
                {
                    Response.Write("Reserva fallida. Espere e inténtelo nuevamente.");
                }
            }
        }

        protected void CargarDatos()
        {

            lstMenu.DataTextField = "Descripcion";
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
                (Session["idMenues"] as List<int>).Remove(id);
                RenderMenuGrid();
            }
        }

        protected void btnAgregarMenu_Click(object sender, EventArgs e)
        {
            (Session["idMenues"] as List<int>).Add(int.Parse(lstMenu.SelectedValue));
            RenderMenuGrid();
        }

        protected bool ValidarDatos(string pNombre, int pPersonas, DateTime pFecha, int pIdMesa)
        {
            return (pNombre != "" && pPersonas > 0 && pPersonas > 0 && pFecha > DateTime.Now && pIdMesa > 0);
        }

        protected void btnMesa_Click(object sender, EventArgs e)
        {
            DateTime mFecha = calFecha.SelectedDate;
            double horas = double.Parse(txtHoras.Text);
            double minutos = double.Parse(txtMinutos.Text);
            mFecha = mFecha.AddHours(horas);
            mFecha = mFecha.AddMinutes(minutos);

            string mesa = (Fachada.Get.BuscarMesaDisponible(int.Parse(txtPersonas.Text), mFecha)).ToString();
            if (mesa != "")
            {
                PanelConfirmar.Visible = true;
                lblMesaData.Text = mesa;
            }
            else
            {
                Response.Write("No se encontró mesa en ese horario. Por favor seleccione un horario distinto.");
            }
        }

        protected void Reset()
        {
            PanelConfirmar.Visible = false;
        }

        protected void LimpiarTextos()
        {
            txtHoras.Text = "";
            txtPersonas.Text = "";
            txtNombre.Text = "";
            txtMinutos.Text = "";
        }

        protected void RenderMenuGrid()
        {
            List<Dominio.Clases.Menu> lProxy = new List<Dominio.Clases.Menu>();
            foreach (int i in (Session["idMenues"] as List<int>))
                lProxy.Add(Fachada.Get.BuscarMenu(i));

            grillaMenus.DataSource = lProxy;
            grillaMenus.DataBind();
        }
    }

}