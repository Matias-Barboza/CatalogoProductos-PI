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

        public void CargarProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            if (articuloNegocio.ObtenerArticulos().Count == 0) 
            {
                NoHayProductosCargados = true;
                return;
            }

            RepeaterAlgunosProductos.DataSource = articuloNegocio.ObtenerArticulos();
            RepeaterAlgunosProductos.DataBind();
        }
    }
}