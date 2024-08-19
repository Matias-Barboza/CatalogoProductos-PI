using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using CatalogoProductos_Web.ClasesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void VincularDatosAUsuario(Usuario usuarioNuevo)
        {
            usuarioNuevo.Nombre = NombreTextBox.Text;
            usuarioNuevo.Apellido = ApellidoTextBox.Text;
            usuarioNuevo.Email = UsuarioTextBox.Text;
            usuarioNuevo.Password = PassTextBox.Text;
        }

        public bool TodasLasValidacionesValidas() 
        {
            // No se tiene en cuenta la validacion de UsuarioExistente
            return NombreValidator.IsValid && ApellidoValidator.IsValid && EmailValidator.IsValid &&
                   EmailValidoValidator.IsValid && PasswordValidator.IsValid && RepetirPasswordValidator.IsValid;
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void RegistrarseButton_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuarioNuevo = new Usuario();

                VincularDatosAUsuario(usuarioNuevo);

                usuarioNuevo.IdUsuario = usuarioNegocio.AñadirUsuario(usuarioNuevo);
                usuarioNuevo.Password = string.Empty;

                Session.Add("UsuarioSesionActual", usuarioNuevo);
                Session.Add("EstablecerDatos", true);

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx");
            }
        }

        //-------------------------------------------------------------------- VALIDATORS ---------------------------------------------------------------------
        protected void UsuarioExistenteCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!TodasLasValidacionesValidas())
                {
                    args.IsValid = true;
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                string emailNuevo = UsuarioTextBox.Text;

                args.IsValid = !usuarioNegocio.UsuarioYaUtilizado(emailNuevo);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}