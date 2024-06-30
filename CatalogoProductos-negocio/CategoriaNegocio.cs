using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM Categorias");

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.Id = accesoDatos.Lector.GetInt32(0);
                    categoria.Descripcion = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);

                    listaCategorias.Add(categoria);
                }

                return listaCategorias;
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
    }
}
