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
                Reset();
            }

        }

        protected void GrillaIngredientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int fila = int.Parse(e.CommandArgument + "");
            string id = (string)GrillaIngredientes.DataKeys[fila].Value;

            if(e.CommandName == "eliminar")
            {
                Fachada.Get.ModificarIngredientesDeMenu((int)Session["idMenu"], id + "", 0);
            }
            ListarMenus();
            ListarIngredientes((int)Session["idMenu"]);
        }

        protected void ListarMenus()
        {
            lstMenu.DataTextField = "Descripcion";
            lstMenu.DataValueField = "Id";
            lstMenu.DataSource = Fachada.Get.ListadoMenuesPorChef((string)Session["Usuario"]);
            lstMenu.DataBind();
        }

        protected void ListarIngredientes(int pIdMenu)
        {
            GrillaIngredientes.DataSource = Fachada.Get.ListadoIngredientesPorMenu(pIdMenu);
            GrillaIngredientes.DataBind();

            lstIngredientes.DataTextField = "Descripcion";
            lstIngredientes.DataValueField= "Codigo";
            lstIngredientes.DataSource = Fachada.Get.ListadoIngredientes();
            lstIngredientes.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(txtCantidad.Text);
            int idMenu = int.Parse(lstMenu.SelectedValue);
            string idIngrediente = lstIngredientes.SelectedValue;

            if (Validar(cantidad))
            {
                Response.Write(Maestra.MensajeError((int)Fachada.Get.ModificarIngredientesDeMenu(idMenu, idIngrediente, cantidad), "Modificar Ingredientes"));
                ListarIngredientes((int)Session["idMenu"]);
            }
            else
            {
                Response.Write("No se puede ingresar esa cantidad. Para eliminar un ingrediente, hágalo con el botón de la grilla");
            }
        }

        protected bool Validar(int pInput)
        {
            return pInput > 0;
        }

        protected void btnCargarMenu_Click(object sender, EventArgs e)
        {
            int idMenu = int.Parse(lstMenu.SelectedValue);
            Session["idMenu"] = idMenu;
            PanelAltaIngrediente.Visible = true;
            ListarIngredientes(idMenu);
        }

        protected void Reset()
        {
            Session["idMenu"] = "";
            PanelAltaIngrediente.Visible = false;
        }
    }
}