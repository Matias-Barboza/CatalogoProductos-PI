using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CatalogoProductos_negocio;
using CatalogoProductos_dominio;

namespace CatalogoProductos_Web
{
    public partial class ListadoProductos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            ProductosGridView.DataSource = articuloNegocio.ObtenerArticulos();
            ProductosGridView.DataBind();
        }
    }
}