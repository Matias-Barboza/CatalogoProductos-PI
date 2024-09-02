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
    public partial class FormularioCategoria : System.Web.UI.Page
    {
        public bool EsEdicion;
        public bool DatosCategoriaCargados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategoriasExistentes();
                Session.Add("DatosCategoriaCargados", false);
            }

            if (Session["DatosCategoriaCargados"] != null)
            {
                DatosCategoriaCargados = (bool)Session["DatosCategoriaCargados"];
            }

            if (Request.QueryString["id"] != null)
            {
                if (!int.TryParse(Request.QueryString["id"], out int id))
                {
                    Response.Redirect("Error.aspx?tipo=6", true);
                }

                EsEdicion = true;

                if (!DatosCategoriaCargados)
                {
                    CargarDatosMarca(id);
                    DatosCategoriaCargados = true;
                    Session.Add("DatosCategoriaCargados", DatosCategoriaCargados);
                }
            }
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarCategoriasExistentes()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> categorias = categoriaNegocio.ObtenerCategorias();

            if (categorias.Count == 0)
            {
                CategoriasExistentesDropDownList.Items.Insert(0, "No se encontraron categorias cargadas...");
                return;
            }

            CategoriasExistentesDropDownList.DataSource = categorias;
            CategoriasExistentesDropDownList.DataTextField = "Descripcion";
            CategoriasExistentesDropDownList.DataValueField = "Id";
            CategoriasExistentesDropDownList.DataBind();
        }

        public void CargarDatosMarca(int id)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            Categoria categoriaACargar = categoriaNegocio.ObtenerCategoriaPorId(id);

            if (categoriaACargar == null)
            {
                Response.Redirect("Error.aspx?tipo=6", true);
            }

            DescripcionTextBox.Text = categoriaACargar.Descripcion;
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void AñadirCategoriaButton_ServerClick(object sender, EventArgs e)
        {
            Categoria categoriaNueva;
            CategoriaNegocio categoriaNegocio;

            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                categoriaNueva = new Categoria();
                categoriaNegocio = new CategoriaNegocio();

                categoriaNueva.Descripcion = DescripcionTextBox.Text;
                categoriaNegocio.AñadirCategoria(categoriaNueva, out int idNuevo);
                categoriaNueva.Id = idNuevo;

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoCategorias.aspx?estado=13", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoCategorias.aspx?estado=14");
            }
        }

        protected void EditarCategoriaButton_ServerClick(object sender, EventArgs e)
        {
            Categoria categoriaAEditar;
            CategoriaNegocio categoriaNegocio;
            bool existeCategoria;

            if (!Page.IsValid)
            {
                return;
            }

            if (!EsEdicion)
            {
                return;
            }

            try
            {
                categoriaNegocio = new CategoriaNegocio();
                int.TryParse(Request.QueryString["id"], out int idCategoria);
                categoriaAEditar = categoriaNegocio.ObtenerCategoriaPorId(idCategoria);
                existeCategoria = categoriaAEditar != null;

                if (!existeCategoria)
                {
                    return;
                }

                categoriaAEditar.Descripcion = DescripcionTextBox.Text;
                categoriaNegocio.ActualizarCategoria(categoriaAEditar);

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoCategorias.aspx?estado=15", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoCategorias.aspx?estado=16");
            }
        }

        //-------------------------------------------------------------------- VALIDATORS ---------------------------------------------------------------------
        protected void DescripcionCategoriaRepetidaCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!DescripcionLongitudValidator.IsValid || !DescripcionObligatoriaValidator.IsValid)
                {
                    args.IsValid = true;
                    return;
                }

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                args.IsValid = !categoriaNegocio.CategoriaYaExiste(DescripcionTextBox.Text);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}