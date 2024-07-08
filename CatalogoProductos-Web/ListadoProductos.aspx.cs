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

            ProductosGridView.DataSource = articuloNegocio.ObtenerArticulos(tipoOrden: "ORDER BY c.Descripcion");
            ProductosGridView.DataBind();
        }

        protected void ProductosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProducto") 
            {
                string id = ProductosGridView.DataKeys[Convert.ToInt32(e.CommandArgument)].ToString();
                Response.Redirect($"FormularioProducto.aspx?id={id}");
            }
        }
    }
}