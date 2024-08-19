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
    public partial class MisFavoritos : System.Web.UI.Page
    {
        public bool NoHayFavoritosGuardados { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarFavoritos();
            }
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarFavoritos() 
        {
            if (Session["UsuarioSesionActual"] == null) 
            {
                return;
            }

            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Usuario usuarioActual = (Usuario) Session["UsuarioSesionActual"];
            List<int> listaIdsArticulosFavoritos = new List<int>();
            List<Articulo> listaArticulosFavoritos = new List<Articulo>();

            listaIdsArticulosFavoritos = favoritoNegocio.ObtenerIdArticulosFavoritosUsuario(usuarioActual.IdUsuario);
            listaArticulosFavoritos = articuloNegocio.ObtenerArticulosPorId(listaIdsArticulosFavoritos);
            NoHayFavoritosGuardados = listaIdsArticulosFavoritos.Count == 0;
            Session.Add("NoHayFavoritosGuardados", NoHayFavoritosGuardados);

            if (NoHayFavoritosGuardados) 
            {
                return;
            }

            FavoritosRepeater.DataSource = listaArticulosFavoritos;
            FavoritosRepeater.DataBind();
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioSesionActual"];
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            Label labelId = (Label)((HtmlButton)(sender)).NamingContainer.FindControl("IdArticuloLabel");
            Favorito favorito = new Favorito()
            {
                IdUsuario = usuario.IdUsuario,
                IdArticulo = int.Parse(labelId.Text)
            };

            if (!favoritoNegocio.ExisteFavorito(favorito))
            {
                favoritoNegocio.AñadirFavorito(favorito);
            }
            else
            {
                favoritoNegocio.EliminarFavorito(favorito);
            }

            CargarFavoritos();
        }
    }
}