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
    public partial class AdminMenuIngrediente : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                if (!master.VerificarUsuario("ADMINISTRADOR"))
                    master.LogOut();
            }
            if (!IsPostBack)
            {
                Cargar();
            }
            
        }

        protected void btnIngrediente_Click(object sender, EventArgs e)
        {

            GrillaIngredientes.DataSource = Fachada.Get.ListadoMenuesConIngrediente(lstIngredientes.SelectedValue);
            GrillaIngredientes.DataBind();

        }

        protected void Cargar()
        {
            lstIngredientes.DataTextField = "Descripcion";
            lstIngredientes.DataValueField = "Codigo";
            lstIngredientes.DataSource = Fachada.Get.ListadoIngredientes();
            lstIngredientes.DataBind();
        }
    }
}