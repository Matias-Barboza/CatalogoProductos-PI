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
        ERROR_INFORMACION_PRODUCTO = 4,
        ERROR_INFORMACION_MARCA = 5,
        ERROR_INFORMACION_CATEGORIA= 6
    }

    public class ErrorHelper
    {
        public const string MENSAJE_ERROR_INTENTO_LOGIN = "Hubo un error en el intento de logueo. Intente nuevamente.";
        public const string MENSAJE_ERROR_INTENTO_REGISTRO = "Hubo un error en el intento de registro. Intente nuevamente.";
        public const string MENSAJE_ERROR_PERMISO_INVALIDO = "Hubo un error en el acceso a la sección debido a los permisos necesarios.";
        public const string MENSAJE_ERROR_INFORMACION_PRODUCTO = "Hubo un error en el acceso a la sección debido a información errónea o faltante del producto.";
        public const string MENSAJE_ERROR_INFORMACION_MARCA = "Hubo un error en el acceso a la sección debido a información errónea o faltante de la marca.";
        public const string MENSAJE_ERROR_INFORMACION_CATEGORIA = "Hubo un error en el acceso a la sección debido a información errónea o faltante de la categoría.";

        public static bool EsErrorContemplado(int tipo) 
        {
            return tipo >= (int) TipoError.ERROR_INTENTO_LOGIN && tipo <= (int) TipoError.ERROR_INFORMACION_CATEGORIA;
        }

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
                case TipoError.ERROR_INFORMACION_MARCA:
                    return MENSAJE_ERROR_INFORMACION_MARCA;
                case TipoError.ERROR_INFORMACION_CATEGORIA:
                    return MENSAJE_ERROR_INFORMACION_CATEGORIA;
                default:
                    return "No deberias estar aquí.";
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
                case TipoError.ERROR_INFORMACION_MARCA:
                    return "ListadoMarcas.aspx";
                case TipoError.ERROR_INFORMACION_CATEGORIA:
                    return "ListadoCategorias.aspx";
                default:
                    return "Default.aspx";
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
                case TipoError.ERROR_INFORMACION_MARCA:
                    return "Volver al listado de marcas";
                case TipoError.ERROR_INFORMACION_CATEGORIA:
                    return "Volver al listado de categorias";
                default:
                    return "Ir al home";
            }
        }
    }
}