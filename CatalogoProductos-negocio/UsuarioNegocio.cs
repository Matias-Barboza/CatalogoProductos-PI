using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public class UsuarioNegocio
    {
        public int AñadirUsuario(Usuario usuario) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"INSERT INTO USERS (email, pass, nombre, apellido, urlImagenPerfil, admin)
                                          OUTPUT INSERTED.id
                                          VALUES (@emailNuevo, @passNuevo, @nombreNuevo, @apellidoNuevo, @urlImagen, @admin)");

                accesoDatos.AgregarParametro("emailNuevo", usuario.Email);
                accesoDatos.AgregarParametro("passNuevo", usuario.Password);
                accesoDatos.AgregarParametro("nombreNuevo", usuario.Nombre);
                accesoDatos.AgregarParametro("apellidoNuevo", usuario.Apellido);
                accesoDatos.AgregarParametro("urlImagen", usuario.UrlImagenPerfil ?? (object) DBNull.Value);
                accesoDatos.AgregarParametro("admin", 0);

                return (int) accesoDatos.EjecutarScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public Usuario ObtenerUsuarioPor(string email, string password) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            Usuario usuarioRecuperado = null;

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, email, nombre, apellido, urlImagenPerfil, admin
                                          FROM USERS
                                          WHERE email = @email AND pass = @password");

                accesoDatos.AgregarParametro("email", email);
                accesoDatos.AgregarParametro("password", password);

                accesoDatos.EjecutarLector();

                if (accesoDatos.Lector.Read())
                {
                    usuarioRecuperado = new Usuario();

                    usuarioRecuperado.IdUsuario = accesoDatos.Lector.GetInt32(0);
                    usuarioRecuperado.Email = accesoDatos.Lector.GetString(1);
                    usuarioRecuperado.Nombre = accesoDatos.Lector.IsDBNull(2) ? null : accesoDatos.Lector.GetString(2);
                    usuarioRecuperado.Apellido = accesoDatos.Lector.IsDBNull(3) ? null : accesoDatos.Lector.GetString(3);
                    usuarioRecuperado.UrlImagenPerfil = accesoDatos.Lector.IsDBNull(4) ? null : accesoDatos.Lector.GetString(4);
                    usuarioRecuperado.EsAdmin = accesoDatos.Lector.GetBoolean(5);
                }

                return usuarioRecuperado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public List<string> ObtenerEmailsUsuarios()
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<string> emailsUsuarios = new List<string>();

            try
            {
                accesoDatos.SetearQuery(@"SELECT email
                                          FROM USERS");

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    emailsUsuarios.Add(accesoDatos.Lector.GetString(0));
                }

                return emailsUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public bool ExisteUsuario(Usuario usuarioComprobar) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT email
                                          FROM USERS
                                          WHERE email = @email");

                accesoDatos.AgregarParametro("email", usuarioComprobar.Email);

                return accesoDatos.EjecutarScalar() != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public bool InformacionCorrectaUsuario(Usuario usuarioComprobar) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT email, pass
                                          FROM USERS
                                          WHERE email = @email AND pass = @pass");

                accesoDatos.AgregarParametro("email", usuarioComprobar.Email);
                accesoDatos.AgregarParametro("pass", usuarioComprobar.Password);

                return accesoDatos.EjecutarScalar() != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public bool EsUsuarioAdmin(Usuario usuarioComprobar) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT admin
                                          FROM USERS
                                          WHERE email = @email");

                accesoDatos.AgregarParametro("email", usuarioComprobar.Email);

                return (bool) accesoDatos.EjecutarScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        // ----------------------------------------------- ADICIONALES ----------------------------------------------------------------------------------------

        public static string ObtenerRutaCompletaImagenPerfil(string rutaParcial) 
        {
            return "~/ImagenesPefiles/" + rutaParcial;
        }

        public bool UsuarioYaUtilizado(string emailNuevo) 
        {
            List<string> listaEmailsExistentes = ObtenerEmailsUsuarios();

            return listaEmailsExistentes.Contains(emailNuevo);
        }
    }
}
