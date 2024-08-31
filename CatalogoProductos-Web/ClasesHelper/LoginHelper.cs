using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using static System.Collections.Specialized.BitVector32;

namespace CatalogoProductos_Web.ClasesHelper
{
    public static class LoginHelper
    {
        public const string PLACEHOLDER_IMAGEN_PERFIL = "https://imebehavioralhealth.com/wp-content/uploads/2021/10/user-icon-placeholder-1.png";

        public static bool PaginaNecesitaSesionIniciada(Page pagina) 
        {
            return pagina is MiPerfil || pagina is MisFavoritos || pagina is Administracion ||
                   pagina is FormularioProducto || pagina is FormularioMarca || pagina is FormularioCategoria ||
                   pagina is ListadoProductos || pagina is ListadoMarcas || pagina is ListadoCategorias;
        }

        public static bool HaySesionIniciada(HttpSessionState session) 
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if (session["UsuarioSesionActual"] == null) 
            {
                return false;
            }

            if (!(session["UsuarioSesionActual"] is Usuario usuario)) 
            {
                return false;
            }

            return usuarioNegocio.ExisteUsuario(usuario);
        }

        public static bool EsUsuarioAdmin(HttpSessionState session) 
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            return usuarioNegocio.EsUsuarioAdmin((Usuario)session["UsuarioSesionActual"]);
        }

        public static bool SesionIniciadaEsAdmin(HttpSessionState session) 
        {
            return HaySesionIniciada(session) && EsUsuarioAdmin(session);
        }

        public static bool DebeSerAdmin(Page pagina) 
        {
            return pagina is Administracion || pagina is ListadoProductos || pagina is ListadoMarcas || pagina is ListadoCategorias ||
                   pagina is FormularioProducto || pagina is FormularioMarca || pagina is FormularioCategoria;
        }

        public static void EliminarDatosSession(HttpSessionState session)
        {
            session.Remove("NoHayFavoritosGuardados");
            session.Remove("DatosArticuloCargados");
            session.Remove("DatosMarcaCargados");
            session.Remove("DatosCategoriaCargados");
            session.Remove("ImagenPorArchivo");
            session.Remove("DebeConfirmarEliminacion");
            session.Remove("EstablecerDatos");
            session.Remove("DebeConfirmarEdicion");
            session.Remove("UsuarioSesionActual");
        }
    }
}