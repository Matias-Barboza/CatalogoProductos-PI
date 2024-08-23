using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using CatalogoProductos_Web.ClasesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        private int _idProducto;

        public bool NoHayOtrosProductosCargados;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si viene vacio debe navegar a pagina de error
            if (Request.QueryString.Count == 0 || !int.TryParse(Request.QueryString["id"], out _idProducto)) 
            {
                Response.Redirect("Error.aspx?tipo=4", true);
            }

            CargarDetalleProducto();
            CargarOtrosProductos();

            if (LoginHelper.HaySesionIniciada(Session)) 
            {
                CargarSiEsFavorito();
            }
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarDetalleProducto() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo;

            articulo = articuloNegocio.ObtenerArticuloPorId(_idProducto);

            TituloProductoLabel.Text = $"{articulo.Marca.Descripcion} {articulo.Nombre}";
            ImagenProducto.ImageUrl = ArticuloNegocio.ConfigurarRutaImagen(articulo.ImagenUrl);
            NombreLabel.Text = articulo.Nombre;
            DescripcionLabel.Text = articulo.Descripcion;
            DescripcionMarcaLabel.Text = articulo.Marca.Descripcion;
            DescripcionCategoriaLabel.Text = articulo.Categoria.Descripcion;
            PrecioLabel.Text = ArticuloNegocio.PrecioFormateado(articulo.Precio);
        }

        public void CargarOtrosProductos() 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.ObtenerArticulosRandom(3, _idProducto);
            articuloNegocio.ConfigurarRutasDeLista(listaArticulos);

            NoHayOtrosProductosCargados = listaArticulos.Count() == 0;

            if (NoHayOtrosProductosCargados)
            {
                return;
            }

            RepeaterAlgunosProductos.DataSource = listaArticulos;
            RepeaterAlgunosProductos.DataBind();
        }

        public void CargarSiEsFavorito() 
        {
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["UsuarioSesionActual"];
            Favorito favorito = new Favorito()
            {
                IdUsuario = usuario.IdUsuario,
                IdArticulo = _idProducto
            };

            if (favoritoNegocio.ExisteFavorito(favorito)) 
            {
                FavIcon.Attributes["class"] = "bi bi-heart-fill nofav-icon-product";
            }
            else 
            {
                FavIcon.Attributes["class"] = "bi bi-heart-fill fav-icon-product";
            }
        }

        public void CargarSiEsFavorito(FavoritoNegocio favoritoNegocio, Favorito favorito)
        {
            if (favoritoNegocio.ExisteFavorito(favorito))
            {
                FavIcon.Attributes["class"] = "bi bi-heart-fill nofav-icon-product";
            }
            else
            {
                FavIcon.Attributes["class"] = "bi bi-heart-fill fav-icon-product";
            }
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {
            if (!LoginHelper.HaySesionIniciada(Session))
            {
                Response.Redirect("Login.aspx", true);
            }

            Usuario usuario = (Usuario)Session["UsuarioSesionActual"];
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            Favorito favorito = new Favorito()
            {
                IdUsuario = usuario.IdUsuario,
                IdArticulo = _idProducto
            };

            if (!favoritoNegocio.ExisteFavorito(favorito))
            {
                favoritoNegocio.AñadirFavorito(favorito);
            }
            else
            {
                favoritoNegocio.EliminarFavorito(favorito);
            }

            CargarSiEsFavorito(favoritoNegocio, favorito);
        }
    }
}