using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurante
{
    public partial class CancelarReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GrillaReserva_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void BtnBuscarReserva_Click(object sender, EventArgs e)
        {

        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {

        }
        protected void Reset()
        {
            PanelConfirmar.Visible = false;
            PanelMostrarReserva.Visible = false;
        }
    }
}