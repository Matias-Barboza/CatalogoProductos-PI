using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public enum EstadoOperacionCategoria
    {
        AÑADIDO_EXITOSO = 13,
        AÑADIDO_FALLIDO = 14,
        EDICION_EXITOSA = 15,
        EDICION_FALLIDA = 16,
        ELIMINACION_EXITOSA = 17,
        ELIMINACION_FALLIDA = 18
    }

    public class CategoriaNegocio
    {
        public bool AñadirCategoria(Categoria categoria, out int idNuevaCategoria)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            idNuevaCategoria = 0;

            try
            {
                accesoDatos.SetearQuery(@"INSERT INTO CATEGORIAS(Descripcion)
                                          OUTPUT INSERTED.id
                                          VALUES (@descripcion)");

                accesoDatos.AgregarParametro("descripcion", categoria.Descripcion);

                idNuevaCategoria = (int)accesoDatos.EjecutarScalar();

                return idNuevaCategoria != 0;
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

        public bool ActualizarCategoria(Categoria categoria)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"UPDATE CATEGORIAS
                                          SET Descripcion = @descripcion
                                          WHERE Id = @id");

                accesoDatos.AgregarParametro("descripcion", categoria.Descripcion);
                accesoDatos.AgregarParametro("id", categoria.Id);

                return accesoDatos.EjecutarQuery() != 0;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public bool CategoriaYaExiste(string nombreCategoriaNueva)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Descripcion
                                          FROM CATEGORIAS
                                          WHERE Descripcion COLLATE Latin1_General_CI_AI LIKE @nombreCategoriaNueva OR
                                          @nombreCategoriaNueva COLLATE Latin1_General_CI_AI LIKE CONCAT('%', Descripcion, '%')");

                accesoDatos.AgregarParametro("nombreCategoriaNueva", nombreCategoriaNueva);

                return (string)accesoDatos.EjecutarScalar() != null;
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

        public Categoria ObtenerCategoriaPorId(int id)
        {
            Categoria categoriaBuscada = null;
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM CATEGORIAS
                                          WHERE Id = @id");

                accesoDatos.AgregarParametro("id", id);

                accesoDatos.EjecutarLector();

                if (accesoDatos.Lector.Read())
                {
                    categoriaBuscada = new Categoria();

                    categoriaBuscada.Id = accesoDatos.Lector.GetInt32(0);
                    categoriaBuscada.Descripcion = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                }

                return categoriaBuscada;
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

        public List<Categoria> ObtenerCategorias()
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM CATEGORIAS");

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

        public List<Categoria> ObtenerCategorias(string tipoOrden = null)
        {
            List<Categoria> listaCategorias = new List<Categoria>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM CATEGORIAS");

                if (!string.IsNullOrEmpty(tipoOrden))
                {
                    accesoDatos.ConcatenarQuery($" {tipoOrden}");
                }

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
