using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aplicacion;
using Dominio;

namespace Restaurante
{
    public partial class AdminReservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                if (!master.VerificarUsuario((int)Session["Rol"]))
                {
                    master.LogOut();
                }

            }

            if (!IsPostBack)
            {
                LimpiarTextos();
            }
        }

        protected void btnFecha_Click(object sender, EventArgs e)
        {
            DateTime fecha = calFecha.SelectedDate;
            fecha.AddHours (double.Parse(txtHora.Text));
            fecha.AddMinutes (int.Parse(txtMinuto.Text));

            GrillaReservas.DataSource = Fachada.Get.ListadoReservasPorFecha(fecha);
            GrillaReservas.DataBind();
            
        }

        protected void LimpiarTextos()
        {
            txtHora.Text = "";
            txtMinuto.Text = "";
        }
    }
}