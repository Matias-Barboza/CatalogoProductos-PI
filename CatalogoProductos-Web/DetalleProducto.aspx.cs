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
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null) 
            {
                CargarDetalleProducto();
            }
        }

        public void CargarDetalleProducto() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo;

            if (!int.TryParse(Request.QueryString["id"], out int id)) 
            {
                return;
            }

            articulo = articuloNegocio.ObtenerArticuloPorId(id);

            ImagenProducto.ImageUrl = articulo.ImagenUrl;
            NombreLabel.Text = articulo.Nombre;
            DescripcionLabel.Text = articulo.Descripcion;
            DescripcionMarcaLabel.Text = articulo.Marca.Descripcion;
            DescripcionCategoriaLabel.Text = articulo.Categoria.Descripcion;
            PrecioLabel.Text = articulo.Precio.ToString("C");
        }
    }
}