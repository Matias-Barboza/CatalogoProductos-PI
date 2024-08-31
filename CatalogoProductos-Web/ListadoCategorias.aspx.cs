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
    public partial class ListadoCategorias : System.Web.UI.Page
    {
        public bool NoHayCategoriasCargadas;
        public bool VieneDeOperacion;
        public string MensajeOperacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["estado"] != null && int.TryParse(Request.QueryString["estado"], out int estado) && Session["VieneDeOperacion"] != null)
                {
                    MostrarEstadoOperacion((EstadoOperacionCategoria)estado);
                }
            }

            CargarListadoCategorias();
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarListadoCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.ObtenerCategorias("ORDER BY Descripcion");
            
            //PARA PROBAR QUE NO LLEGAN CATEGORIAS
            //List<Categoria> categorias = new List<Categoria>();

            if (categorias.Count == 0)
            {
                NoHayCategoriasCargadas = true;
                return;
            }

            CategoriasGridView.DataSource = categorias;
            CategoriasGridView.DataBind();
        }

        public void MostrarEstadoOperacion(EstadoOperacionCategoria estado)
        {
            VieneDeOperacion = (bool)Session["VieneDeOperacion"];
            bool OperacionExitosa = false;

            switch (estado)
            {
                case EstadoOperacionCategoria.AÑADIDO_EXITOSO:
                    MensajeOperacion = "Categoría añadida de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionCategoria.AÑADIDO_FALLIDO:
                    MensajeOperacion = "Hubo un error en el añadido de la categoría.";
                    break;
                case EstadoOperacionCategoria.EDICION_EXITOSA:
                    MensajeOperacion = "Categoría editada de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionCategoria.EDICION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la edición de la categoría.";
                    break;
                case EstadoOperacionCategoria.ELIMINACION_EXITOSA:
                    MensajeOperacion = "Categoría eliminada de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionCategoria.ELIMINACION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la eliminación de la categoría.";
                    break;
                default:
                    VieneDeOperacion = false;
                    break;
            }

            if (OperacionExitosa)
            {
                MensajeOperacionLabel.CssClass = "operation-success";
            }
            else
            {
                MensajeOperacionLabel.CssClass = "operation-failed";
            }

            MensajeOperacionLabel.Text = MensajeOperacion;

            Session.Remove("VieneDeOperacion");
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void CategoriasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CategoriasGridView.PageIndex = e.NewPageIndex;
            CategoriasGridView.DataBind();
        }

        protected void CategoriasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarCategoria")
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                string id = CategoriasGridView.DataKeys[indiceFila].Value.ToString();

                Response.Redirect($"FormularioCategoria.aspx?id={id}");
            }
        }
    }
}