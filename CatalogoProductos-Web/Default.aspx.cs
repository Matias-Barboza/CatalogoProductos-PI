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
    public partial class Default : System.Web.UI.Page
    {
        public bool NoHayProductosCargados { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarProductos();
            }
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.ObtenerArticulosRandom(cantidadArticulosSolicitados: 6);
            articuloNegocio.ConfigurarRutasDeLista(listaArticulos); 

            if (listaArticulos.Count == 0) 
            {
                NoHayProductosCargados = true;
                return;
            }

            RepeaterAlgunosProductos.DataSource = listaArticulos;
            RepeaterAlgunosProductos.DataBind();
        }
    }
}