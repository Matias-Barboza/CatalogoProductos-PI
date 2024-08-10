using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using CatalogoProductos_dominio;
using CatalogoProductos_negocio;

namespace CatalogoProductos_Web.ClasesHelper
{
    public static class LoginHelper
    {
        public static bool PaginaNecesitaSesionIniciada(Page pagina) 
        {
            return pagina is ListadoProductos || pagina is MiPerfil || pagina is MisFavoritos || pagina is FormularioProducto;
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

        public static bool DebeSerAdmin(Page page) 
        {
            return page is ListadoProductos || page is FormularioProducto;
        }
    }
}