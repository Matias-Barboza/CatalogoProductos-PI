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
        public const string MENSAJE_ERROR_INTENTO_REGISTRO = "Hubo un error en el intento de Registro. Intente nuevamente.";
        public const string MENSAJE_ERROR_PERMISO_INVALIDO = "Hubo un error en el acceso a esta sección debido a los permisos necesarios.";
        public const string MENSAJE_ERROR_INFORMACION_PRODUCTO = "Hubo un error en el acceso a esta sección debido a información erronea del producto.";
    }
}