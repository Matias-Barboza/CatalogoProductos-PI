using CatalogoProductos_Web.ClasesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si viene con algun error
            if (Request.QueryString["tipo"] != null && int.TryParse(Request.QueryString["tipo"], out int tipo)) 
            {
                CargarTipoError((TipoError) tipo);
                return;
            }

            //Si viene vacio o tipo invalido
            Response.Redirect("Default.aspx");
        }

        //------------------------------------------------------- METÓDOS -------------------------------------------------------------------------------------
        public void CargarTipoError(TipoError tipoError) 
        {
            MensajeErrorParagraph.InnerText = ErrorHelper.ObtenerMensajeError(tipoError);
            LinkPostError.HRef = ErrorHelper.ObtenerLinkPostError(tipoError);
            MensajeLinkPostErrorLabel.Text = ErrorHelper.ObtenerMensajeLinkPostError(tipoError);
        }
    }
}