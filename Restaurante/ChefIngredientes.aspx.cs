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
    public partial class IngredientesChef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                if (!master.VerificarUsuario("CHEF"))
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
            int fila = int.Parse(e.CommandArgument + "");
            int id = (int)GrillaIngredientes.DataKeys[fila].Value;

            if(e.CommandName == "eliminar")
            {
                Fachada.Get.ModificarIngredientesDeMenu(id + "");
            }
        }


        protected void btnCargarMenu_Click(object sender, EventArgs e)
        {
            ListarMenus();
            int idMenu = int.Parse(lstMenu.SelectedItem.Value);
            ListarIngredientes(idMenu);
        }

        protected void ListarMenus()
        {

            lstMenu.DataSource = Fachada.Get.ListadoMenuesPorChef((string)Session["Usuario"]);
            lstMenu.DataBind();
        }

        protected void ListarIngredientes(int pIdMenu)
        {
            

            lstIngredientes.DataTextField = "Descripcion";
            lstIngredientes.DataValueField= "Codigo";
            lstIngredientes.DataSource = Fachada.Get.ListadoIngredientesPorMenu(pIdMenu);
            lstIngredientes.DataBind();
        }

        protected void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMenu = int.Parse(lstMenu.SelectedItem.Value);
            ListarIngredientes(idMenu);
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtCantidad.Text);
            int idMenu = int.Parse(lstMenu.SelectedValue);
            int idIngrediente = int.Parse(lstIngredientes.SelectedValue);

            if (Validar(cantidad))
            {
                //Fachada.Get.ModificarIngredientesDeMenu(idMenu, idIngrediente, cantidad);
            }
        }

        protected bool Validar(int pInput)
        {
            return pInput > 0;
        }
    }
}