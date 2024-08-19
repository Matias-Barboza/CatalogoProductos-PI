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
        public bool VieneDeOperacion;
        public string MensajeOperacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (Request.QueryString["estado"] != null && int.TryParse(Request.QueryString["estado"], out int estado) && Session["VieneDeOperacion"] != null) 
                {
                    MostrarEstadoOperacion((EstadoOperacion) estado);
                }
            }

            CargarListadoProductos();
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarListadoProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> articulos = articuloNegocio.ObtenerArticulos(tipoOrden: "ORDER BY c.Descripcion");

            if (articulos.Count == 0) 
            {
                NoHayProductosCargados = true;
                return;
            }

            articuloNegocio.ConfigurarPrecios(articulos);

            ProductosGridView.DataSource = articulos;
            ProductosGridView.DataBind();
        }

        public void MostrarEstadoOperacion(EstadoOperacion estado) 
        {
            VieneDeOperacion = (bool)Session["VieneDeOperacion"];
            bool OperacionExitosa = false;

            switch (estado) 
            {
                case EstadoOperacion.AÑADIDO_EXITOSO:
                    MensajeOperacion = "Producto añadido de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacion.AÑADIDO_FALLIDO:
                    MensajeOperacion = "Hubo un error en el añadido del producto.";
                    break;
                case EstadoOperacion.EDICION_EXITOSA:
                    MensajeOperacion = "Producto editado de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacion.EDICION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la edición del producto.";
                    break;
                case EstadoOperacion.ELIMINACION_EXITOSA:
                    MensajeOperacion = "Producto eliminado de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacion.ELIMINACION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la eliminación del producto.";
                    break;
                default:
                    VieneDeOperacion = false;
                    break;
            }

            if (OperacionExitosa) 
            {
                MensajeOperacionLabel.CssClass = "operation-success";
            }
            else 
            {
                MensajeOperacionLabel.CssClass = "operation-failed";
            }

            MensajeOperacionLabel.Text = MensajeOperacion;

            Session.Remove("VieneDeOperacion");
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void ProductosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProducto") 
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                string id = ProductosGridView.DataKeys[indiceFila].Value.ToString();

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