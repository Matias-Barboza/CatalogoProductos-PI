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
    public partial class FormularioMarca : System.Web.UI.Page
    {
        public bool EsEdicion;
        public bool DatosMarcaCargados;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMarcasExistentes();
                Session.Add("DatosMarcaCargados", false);
            }

            if (Session["DatosMarcaCargados"] != null)
            {
                DatosMarcaCargados = (bool)Session["DatosMarcaCargados"];
            }

            if (Request.QueryString["id"] != null)
            {
                if (!int.TryParse(Request.QueryString["id"], out int id))
                {
                    Response.Redirect("Error.aspx?tipo=5", true);
                }

                EsEdicion = true;

                if (!DatosMarcaCargados)
                {
                    CargarDatosMarca(id);
                    DatosMarcaCargados = true;
                    Session.Add("DatosMarcaCargados", DatosMarcaCargados);
                }
            }
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarMarcasExistentes() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.ObtenerMarcas();

            if (marcas.Count == 0)
            {
                MarcasExistentesDropDownList.Items.Insert(0, "No se encontraron marcas cargadas...");
                return;
            }

            MarcasExistentesDropDownList.DataSource = marcas;
            MarcasExistentesDropDownList.DataTextField = "Descripcion";
            MarcasExistentesDropDownList.DataValueField = "Id";
            MarcasExistentesDropDownList.DataBind();
        }

        public void CargarDatosMarca(int id) 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            Marca marcaACargar = marcaNegocio.ObtenerMarcaPorId(id);

            if (marcaACargar == null) 
            {
                Response.Redirect("Error.aspx?tipo=5", true);
            }

            DescripcionTextBox.Text = marcaACargar.Descripcion;
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void AñadirMarcaButton_ServerClick(object sender, EventArgs e)
        {
            Marca marcaNueva;
            MarcaNegocio marcaNegocio;

            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                marcaNueva = new Marca();
                marcaNegocio = new MarcaNegocio();

                marcaNueva.Descripcion = DescripcionTextBox.Text;
                marcaNegocio.AñadirMarca(marcaNueva, out int idNuevo);
                marcaNueva.Id = idNuevo;

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoMarcas.aspx?estado=7", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoMarcas.aspx?estado=8");
            }
        }

        protected void EditarMarcaButton_ServerClick(object sender, EventArgs e)
        {
            Marca marcaAEditar;
            MarcaNegocio marcaNegocio;
            bool existeMarca;

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
                marcaNegocio = new MarcaNegocio();
                int.TryParse(Request.QueryString["id"], out int idMarca);
                marcaAEditar = marcaNegocio.ObtenerMarcaPorId(idMarca);
                existeMarca = marcaAEditar != null;

                if (!existeMarca)
                {
                    return;
                }

                marcaAEditar.Descripcion = DescripcionTextBox.Text;
                marcaNegocio.ActualizarMarca(marcaAEditar);

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoMarcas.aspx?estado=9", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoMarcas.aspx?estado=10");
            }
        }

        //-------------------------------------------------------------------- VALIDATORS ---------------------------------------------------------------------
        protected void DescripcionMarcaRepetidaCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!DescripcionLongitudValidator.IsValid || !DescripcionObligatoriaValidator.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                MarcaNegocio marcaNegocio = new MarcaNegocio();
                
                args.IsValid = !marcaNegocio.MarcaYaExiste(DescripcionTextBox.Text);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}