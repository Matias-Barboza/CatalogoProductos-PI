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
        public bool NoHayProductosCargados {  get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarProductos();
            }

            if (Session["NoHayProductosCargados"] != null) 
            {
                NoHayProductosCargados = (bool)Session["NoHayProductosCargados"];
            }
        }

        public void CargarProductos(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "", string campoBusqueda = "") 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.ObtenerArticulos(marcas, categorias, condicionPrecio, precio, tipoOrden, campoBusqueda);

            NoHayProductosCargados = listaArticulos.Count == 0;
            Session.Add("NoHayProductosCargados", NoHayProductosCargados);
            RepeaterProductos.DataSource = listaArticulos;
            RepeaterProductos.DataBind();
        }

        public void AplicarFiltros(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "", string campoBusqueda = "") 
        {
            CargarProductos(marcas, categorias, condicionPrecio, precio, tipoOrden, campoBusqueda);
        }

        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {

        }
    }
}