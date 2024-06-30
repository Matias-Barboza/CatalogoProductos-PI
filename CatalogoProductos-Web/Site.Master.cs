using CatalogoProductos_negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (Page is Productos) 
                {
                    CargarFiltros();
                }
            }
        }

        public void CargarFiltros() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            PruebaFiltroDropDownList.DataSource = marcaNegocio.ObtenerMarcas();
            PruebaFiltroDropDownList.DataTextField = "Descripcion";
            PruebaFiltroDropDownList.DataValueField = "Id";
            PruebaFiltroDropDownList.DataBind();
        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            
        }

        protected void AplicarFiltrosButton_ServerClick(object sender, EventArgs e)
        {

        }
        protected void LimpiarFiltrosButton_ServerClick(object sender, EventArgs e)
        {

        }
    }
}