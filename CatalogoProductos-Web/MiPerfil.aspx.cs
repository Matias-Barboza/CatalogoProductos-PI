using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using CatalogoProductos_Web.ClasesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace CatalogoProductos_Web
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        public bool DebeConfirmarEdicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                CargarDatosUsuario();
                Session.Add("DebeConfirmarEdicion", false);
            }

            if (Session["DebeConfirmarEdicion"] != null)
            {
                DebeConfirmarEdicion = (bool) Session["DebeConfirmarEdicion"];
            }

            CambiarEstadoTextBoxs();
        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void CargarDatosUsuario() 
        {
            if (Session["UsuarioSesionActual"] == null) 
            {
                return;
            }

            Usuario usuarioActual = (Usuario)Session["UsuarioSesionActual"];

            EmailTextBox.Text = usuarioActual.Email;
            NombreActualTextBox.Text = !string.IsNullOrEmpty(usuarioActual.Nombre) ? usuarioActual.Nombre : "No hay nombre establecido";
            ApellidoActualTextBox.Text = !string.IsNullOrEmpty(usuarioActual.Apellido) ? usuarioActual.Apellido : "No hay apellido establecido";
            ImagenActualImage.ImageUrl = usuarioActual.UrlImagenPerfil != null ? 
                                         UsuarioNegocio.ObtenerRutaCompletaImagenPerfil(usuarioActual.UrlImagenPerfil) : LoginHelper.PLACEHOLDER_IMAGEN_PERFIL;
        }

        public void CambiarEstadoEdicion(bool estado) 
        {
            DebeConfirmarEdicion = estado;

            Session.Add("DebeConfirmarEdicion", DebeConfirmarEdicion);
        }

        public void ActualizarUsuario(bool quitarImagenPerfil = false)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuarioActual = (Usuario)Session["UsuarioSesionActual"];

            if (!quitarImagenPerfil)
            {
                usuarioActual.Nombre = NombreActualTextBox.Text;
                usuarioActual.Apellido = ApellidoActualTextBox.Text;
            }
            usuarioActual.UrlImagenPerfil = ObtenerRutaImagenPerfil(usuarioActual, quitarImagenPerfil);

            usuarioNegocio.ActualizarUsuario(usuarioActual);
        }

        public void ActualizarEncabezadoPerfilMasterPage()
        {
            Site masterPage = Master as Site;

            masterPage.EstablecerNombrePerfil();
            masterPage.EstablecerFotoPerfil();
        }

        public void CambiarEstadoTextBoxs()
        {
            NombreActualTextBox.Enabled = DebeConfirmarEdicion;
            ApellidoActualTextBox.Enabled = DebeConfirmarEdicion;
        }

        //-------------------------------------------------------------------- FUNCIONES ----------------------------------------------------------------------
        public string ObtenerRutaImagenPerfil(Usuario usuarioActual, bool quitarImagenPerfil)
        {
            if (ImagenPerfilInput.PostedFile.FileName != "")
            {
                string rutaAGuardar;
                string rutaACarpetaImagenesPerfiles = Server.MapPath("./ImagenesPerfiles/");
                string rutaCompleta;

                rutaAGuardar = $"imagenPerfilUsuario{usuarioActual.IdUsuario}.jpg";
                rutaCompleta = rutaACarpetaImagenesPerfiles + rutaAGuardar;

                ImagenPerfilInput.PostedFile.SaveAs(rutaCompleta);

                return rutaAGuardar;
            }

            return quitarImagenPerfil ? null : usuarioActual.UrlImagenPerfil;
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void EditarUsuarioButton_ServerClick(object sender, EventArgs e)
        {
            CambiarEstadoEdicion(true);
            CambiarEstadoTextBoxs();
        }

        protected void ConfirmarEdicionButton_ServerClick(object sender, EventArgs e)
        {
            if (!Page.IsValid) 
            {
                return;
            }

            ActualizarUsuario();
            ActualizarEncabezadoPerfilMasterPage();

            CambiarEstadoEdicion(false);
            CambiarEstadoTextBoxs();
            CargarDatosUsuario();
        }
        protected void CancelarEdicionButton_ServerClick(object sender, EventArgs e)
        {
            CargarDatosUsuario();
            CambiarEstadoEdicion(false);
        }
        protected void QuitarFotoButton_Click(object sender, EventArgs e)
        {
            ActualizarUsuario(true);
            ActualizarEncabezadoPerfilMasterPage();

            CambiarEstadoEdicion(false);
            CambiarEstadoTextBoxs();
            CargarDatosUsuario();
        }

        //-------------------------------------------------------------------- VALIDATORS ----------------------------------------------------------------------
        protected void TipoArchivoCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = ImagenPerfilInput.PostedFile.FileName.EndsWith(".jpg") || ImagenPerfilInput.PostedFile.FileName.EndsWith(".png");
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}