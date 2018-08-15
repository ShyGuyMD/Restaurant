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
    public partial class AdminReservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                master.VerificarUsuario("ADMINISTRADOR");

            }
        }

        protected void btnFecha_Click(object sender, EventArgs e)
        {
            DateTime fecha = calFecha.SelectedDate;
            List <Reserva> res = Fachada.Get.ListadoReservasPorFecha(fecha);

            if (res.Count >= 1)
            {
                GrillaReservas.DataSource = res;
                GrillaReservas.DataBind();
            }
            else
            {
                Response.Write("No se encontraron reservas en la fecha seleccionada.");
            }
           
            
        }
        
    }
}