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
    public partial class AdminMenuPrecio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var master = Master as Maestra;
            if (master != null)
            {
                master.VerificarUsuario(0);

            }

            if (!IsPostBack)
            {
                ListarMenues();
            }
        }
        protected void ListarMenues()
        {

            GrillaMenus.DataSource = Fachada.Get.ListadoMenuesConPrecio();
            GrillaMenus.DataBind();
        }

    }
}


            
           

        