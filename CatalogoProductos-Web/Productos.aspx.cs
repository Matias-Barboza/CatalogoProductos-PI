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

            if (LoginHelper.HaySesionIniciada(Session))
            {
                CargarFavoritos();
            }
        }

        public void CargarProductos(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "", string campoBusqueda = "") 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = articuloNegocio.ObtenerArticulos(marcas, categorias, condicionPrecio, precio, tipoOrden, campoBusqueda);
            articuloNegocio.ConfigurarRutasDeLista(listaArticulos);

            NoHayProductosCargados = listaArticulos.Count == 0;
            Session.Add("NoHayProductosCargados", NoHayProductosCargados);
            RepeaterProductos.DataSource = listaArticulos;
            RepeaterProductos.DataBind();
        }

        public void AplicarFiltros(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "", string campoBusqueda = "") 
        {
            CargarProductos(marcas, categorias, condicionPrecio, precio, tipoOrden, campoBusqueda);
        }

        public void CargarFavoritos() 
        {
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["UsuarioSesionActual"];
            List<int> listaIdArticulosFavoritos = favoritoNegocio.ObtenerIdArticulosFavoritosUsuario(usuario.IdUsuario);

            if (listaIdArticulosFavoritos.Count == 0) 
            {
                return;
            }

            foreach (RepeaterItem item in RepeaterProductos.Items)
            {
                HtmlGenericControl icon = (HtmlGenericControl) item.FindControl("FavIcon");
                Label labelIdArticulo = (Label) item.FindControl("IdArticuloLabel");
                int idArticulo = int.Parse(labelIdArticulo.Text);

                if (listaIdArticulosFavoritos.Contains(idArticulo)) 
                {
                    icon.Attributes["class"] = "bi bi-heart-fill nofav-icon-product";
                }
                else 
                {
                    icon.Attributes["class"] = "bi bi-heart-fill fav-icon-product";
                }
            }
        }

        protected void FavoritoButton_ServerClick(object sender, EventArgs e)
        {
            if (!LoginHelper.HaySesionIniciada(Session))
            {
                Response.Redirect("Login.aspx?pagina=productos", true);
            }

            Usuario usuario = (Usuario)Session["UsuarioSesionActual"];
            Label labelIdArticulo = (Label)((HtmlButton)(sender)).NamingContainer.FindControl("IdArticuloLabel");
            FavoritoNegocio favoritoNegocio = new FavoritoNegocio();
            Favorito favorito = new Favorito()
            {
                IdUsuario = usuario.IdUsuario,
                IdArticulo = int.Parse(labelIdArticulo.Text)
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