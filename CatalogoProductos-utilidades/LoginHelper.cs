using CatalogoProductos_Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace CatalogoProductos_utilidades
{
    public static class LoginHelper
    {
        public static bool PaginaNecesitaSesionIniciada(Page pagina) 
        {
            return pagina is ListadoProductos || pagina is MiPerfil;
        }
    }
}
