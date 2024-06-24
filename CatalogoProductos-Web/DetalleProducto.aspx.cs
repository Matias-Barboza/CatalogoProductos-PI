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
        private int _idProducto;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null) 
            {
                if (!int.TryParse(Request.QueryString["id"], out _idProducto))
                {
                    return;
                }

                CargarDetalleProducto();
                CargarOtrosProductos();
            }
        }

        public void CargarDetalleProducto() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo;

            articulo = articuloNegocio.ObtenerArticuloPorId(_idProducto);

            TituloProductoLabel.Text = $"{articulo.Marca.Descripcion} {articulo.Nombre}";
            ImagenProducto.ImageUrl = articulo.ImagenUrl;
            NombreLabel.Text = articulo.Nombre;
            DescripcionLabel.Text = articulo.Descripcion;
            DescripcionMarcaLabel.Text = articulo.Marca.Descripcion;
            DescripcionCategoriaLabel.Text = articulo.Categoria.Descripcion;
            PrecioLabel.Text = articulo.Precio.ToString("C");
        }

        public void CargarOtrosProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            RepeaterAlgunosProductos.DataSource = articuloNegocio.ObtenerArticulosRandom(3, _idProducto);
            RepeaterAlgunosProductos.DataBind();
        }

        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {

        }
    }
}