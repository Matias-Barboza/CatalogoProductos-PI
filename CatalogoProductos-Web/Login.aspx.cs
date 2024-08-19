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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (LoginHelper.HaySesionIniciada(Session)) 
                {
                    Response.Redirect("MiPerfil.aspx",true);
                }
            }
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void IngresarButton_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario() {
                Email = UsuarioTextBox.Text,
                Password = PassTextBox.Text
            };

            if (!Page.IsValid) 
            {
                return;
            }

            try
            {
                usuario = usuarioNegocio.ObtenerUsuarioPor(usuario.Email, usuario.Password);

                Session.Add("UsuarioSesionActual", usuario);
                Session.Add("EstablecerDatos", true);

                if (Request.QueryString["pagina"] != null) 
                {
                    Response.Redirect("Productos.aspx", true);
                }

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception)
            {
                Response.Redirect("Error.aspx", false);
            }
        }

        //-------------------------------------------------------------------- VALIDATORS ---------------------------------------------------------------------
        protected void UsuarioCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!UsuarioValidatorLogin.IsValid || !PassValidatorLogin.IsValid) 
                {
                    args.IsValid = true;
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario()
                {
                    Email = UsuarioTextBox.Text,
                    Password = PassTextBox.Text
                };

                args.IsValid = usuarioNegocio.ExisteUsuario(usuario);

                if (!args.IsValid) 
                {
                    DatosUsuarioCustomValidator.IsValid = true;
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void DatosUsuarioCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!UsuarioValidatorLogin.IsValid || !PassValidatorLogin.IsValid)
                {
                    args.IsValid = true;
                    return;
                }

                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuario = new Usuario()
                {
                    Email = UsuarioTextBox.Text,
                    Password = PassTextBox.Text
                };

                args.IsValid = usuarioNegocio.InformacionCorrectaUsuario(usuario);

                if (!UsuarioCustomValidator.IsValid) 
                {
                    args.IsValid = true;
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}