using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatalogoProductos_Web.ClasesHelper
{
    public enum TipoError 
    {
        ERROR_INTENTO_LOGIN = 1,
        ERROR_INTENTO_REGISTRO = 2,
        ERROR_PERMISO_INVALIDO = 3,
        ERROR_INFORMACION_PRODUCTO = 4
    }

    public class ErrorHelper
    {
        public const string MENSAJE_ERROR_INTENTO_LOGIN = "Hubo un error en el intento de logueo. Intente nuevamente.";
        public const string MENSAJE_ERROR_INTENTO_REGISTRO = "Hubo un error en el intento de registro. Intente nuevamente.";
        public const string MENSAJE_ERROR_PERMISO_INVALIDO = "Hubo un error en el acceso a la sección debido a los permisos necesarios.";
        public const string MENSAJE_ERROR_INFORMACION_PRODUCTO = "Hubo un error en el acceso a la sección debido a información errónea o faltante del producto.";

        public static string ObtenerMensajeError(TipoError tipoError) 
        {
            switch(tipoError) 
            {
                case TipoError.ERROR_INTENTO_LOGIN:
                    return MENSAJE_ERROR_INTENTO_LOGIN;
                case TipoError.ERROR_INTENTO_REGISTRO:
                    return MENSAJE_ERROR_INTENTO_REGISTRO;
                case TipoError.ERROR_PERMISO_INVALIDO:
                    return MENSAJE_ERROR_PERMISO_INVALIDO;
                case TipoError.ERROR_INFORMACION_PRODUCTO:
                    return MENSAJE_ERROR_INFORMACION_PRODUCTO;
                default:
                    return null;
            }
        }

        public static string ObtenerLinkPostError(TipoError tipoError)
        {
            switch (tipoError)
            {
                case TipoError.ERROR_INTENTO_LOGIN:
                    return "Login.aspx";
                case TipoError.ERROR_INTENTO_REGISTRO:
                    return "FormularioRegistro.aspx";
                case TipoError.ERROR_PERMISO_INVALIDO:
                    return "Default.aspx";
                case TipoError.ERROR_INFORMACION_PRODUCTO:
                    return "Productos.aspx";
                default:
                    return null;
            }
        }

        public static string ObtenerMensajeLinkPostError(TipoError tipoError)
        {
            switch (tipoError)
            {
                case TipoError.ERROR_INTENTO_LOGIN:
                    return "Volver al login";
                case TipoError.ERROR_INTENTO_REGISTRO:
                    return "Volver al formulario de registro";
                case TipoError.ERROR_PERMISO_INVALIDO:
                    return "Ir al home";
                case TipoError.ERROR_INFORMACION_PRODUCTO:
                    return "Ir a ver productos";
                default:
                    return null;
            }
        }
    }
}