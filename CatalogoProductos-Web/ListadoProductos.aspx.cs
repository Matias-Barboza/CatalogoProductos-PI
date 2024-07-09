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
        public bool NoHayProductosCargados;

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarListadoProductos();
        }

        public void CargarListadoProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> articulos = articuloNegocio.ObtenerArticulos(tipoOrden: "ORDER BY c.Descripcion");

            if (articulos.Count == 0) 
            {
                NoHayProductosCargados = true;
                return;
            }

            ProductosGridView.DataSource = articulos;
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

        protected void ProductosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductosGridView.PageIndex = e.NewPageIndex;
            ProductosGridView.DataBind();
        }
    }
}