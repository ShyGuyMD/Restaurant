using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurante
{
    public partial class IngredientesChef : System.Web.UI.Page
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
                ListarMenus();
                
            }
        }

        protected void GrillaIngredientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GrillaIngredientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCargarMenu_Click(object sender, EventArgs e)
        {
            GrillaIngredientes.DataBind();
        }
    }
}