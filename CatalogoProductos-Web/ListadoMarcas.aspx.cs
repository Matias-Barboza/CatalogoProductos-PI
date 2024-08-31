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
    public partial class ListadoMarcas : System.Web.UI.Page
    {
        public bool NoHayMarcasCargadas;
        public bool VieneDeOperacion;
        public string MensajeOperacion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["estado"] != null && int.TryParse(Request.QueryString["estado"], out int estado) && Session["VieneDeOperacion"] != null)
                {
                    MostrarEstadoOperacion((EstadoOperacionMarca)estado);
                }
            }

            CargarListadoMarcas();
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarListadoMarcas() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.ObtenerMarcas("ORDER BY Descripcion");
            
            //PARA PROBAR QUE NO LLEGAN MARCAS
            //List<Marca> marcas = new List<Marca>();

            if (marcas.Count == 0 ) 
            {
                NoHayMarcasCargadas = true;
                return;
            }

            MarcasGridView.DataSource = marcas;
            MarcasGridView.DataBind();
        }

        public void MostrarEstadoOperacion(EstadoOperacionMarca estado) 
        {
            VieneDeOperacion = (bool)Session["VieneDeOperacion"];
            bool OperacionExitosa = false;

            switch (estado)
            {
                case EstadoOperacionMarca.AÑADIDO_EXITOSO:
                    MensajeOperacion = "Marca añadida de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionMarca.AÑADIDO_FALLIDO:
                    MensajeOperacion = "Hubo un error en el añadido de la marca.";
                    break;
                case EstadoOperacionMarca.EDICION_EXITOSA:
                    MensajeOperacion = "Marca editada de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionMarca.EDICION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la edición de la marca.";
                    break;
                case EstadoOperacionMarca.ELIMINACION_EXITOSA:
                    MensajeOperacion = "Marca eliminada de forma exitosa.";
                    OperacionExitosa = true;
                    break;
                case EstadoOperacionMarca.ELIMINACION_FALLIDA:
                    MensajeOperacion = "Hubo un error en la eliminación de la marca.";
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
        protected void MarcasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MarcasGridView.PageIndex = e.NewPageIndex;
            MarcasGridView.DataBind();
        }

        protected void MarcasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarMarca") 
            {
                int indiceFila = Convert.ToInt32(e.CommandArgument);
                string id = MarcasGridView.DataKeys[indiceFila].Value.ToString();

                Response.Redirect($"FormularioMarca.aspx?id={id}");
            }
        }
    }
}