using CatalogoProductos_negocio;
using CatalogoProductos_dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class FormularioProducto : System.Web.UI.Page
    {
        public bool EsEdicion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si viene vacio debe navegar a pagina de error
            if (!IsPostBack) 
            {
                CargarDesplegables();
            }

            if (Request.QueryString["id"] != null) 
            {
                EsEdicion = true;
            }
        }

        public void CargarDesplegables() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            MarcasDropDownList.DataSource = marcaNegocio.ObtenerMarcas();
            MarcasDropDownList.DataTextField = "Descripcion";
            MarcasDropDownList.DataValueField = "Id";
            MarcasDropDownList.DataBind();

            CategoriasDropDownList.DataSource = categoriaNegocio.ObtenerCategorias();
            CategoriasDropDownList.DataTextField = "Descripcion";
            CategoriasDropDownList.DataValueField = "Id";
            CategoriasDropDownList.DataBind();
        }

        protected void AñadirArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void EditarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void EliminarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void ProbarUrlButton_Click(object sender, EventArgs e)
        {
            NuevaImagen.ImageUrl = UrlImagenTextBox.Text;
        }
    }
}