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
    public partial class AdminMenuIngrediente : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                master.VerificarUsuario(0);
            }

            lstIngredientes.DataTextField = "Datos";
            lstIngredientes.DataValueField = "Id";
            lstIngredientes.DataSource = Fachada.Get.ListadoMenuesConPrecio();
            lstIngredientes.DataBind();
        }

        protected void btnIngrediente_Click(object sender, EventArgs e)
        {
            GrillaIngredientes.DataSource = Fachada.Get.ListadoMenuesConIngrediente(lstIngredientes.SelectedValue);
            GrillaIngredientes.DataBind();

        }
    }
}