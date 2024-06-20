using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarProductos();
            }
        }

        public void CargarProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            RepeaterProductos.DataSource = articuloNegocio.ObtenerArticulos();
            RepeaterProductos.DataBind();
        }
    }
}