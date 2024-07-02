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
        }

        public void CargarProductos(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "") 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            
            if (articuloNegocio.ObtenerArticulos(marcas, categorias, condicionPrecio, precio, tipoOrden).Count == 0) 
            {
                NoHayProductosCargados = true;
                return;
            }

            RepeaterProductos.DataSource = articuloNegocio.ObtenerArticulos(marcas, categorias, condicionPrecio, precio, tipoOrden);
            RepeaterProductos.DataBind();
        }

        public void AplicarFiltros(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "") 
        {
            CargarProductos(marcas, categorias, condicionPrecio, precio, tipoOrden);
        }

        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {

        }
    }
}