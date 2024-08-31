using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public enum EstadoOperacionMarca 
    {
        AÑADIDO_EXITOSO = 7,
        AÑADIDO_FALLIDO = 8,
        EDICION_EXITOSA = 9,
        EDICION_FALLIDA = 10,
        ELIMINACION_EXITOSA = 11,
        ELIMINACION_FALLIDA = 12
    }

    public class MarcaNegocio
    {
        public bool AñadirMarca(Marca marca, out int idNuevaMarca) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            idNuevaMarca = 0;

            try
            {
                accesoDatos.SetearQuery(@"INSERT INTO MARCAS(Descripcion)
                                          OUTPUT INSERTED.id
                                          VALUES (@descripcion)");

                accesoDatos.AgregarParametro("descripcion", marca.Descripcion);

                idNuevaMarca = (int) accesoDatos.EjecutarScalar();

                return idNuevaMarca != 0;
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

        public bool ActualizarMarca(Marca marca) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"UPDATE MARCAS
                                          SET Descripcion = @descripcion
                                          WHERE Id = @id");

                accesoDatos.AgregarParametro("descripcion", marca.Descripcion);
                accesoDatos.AgregarParametro("id", marca.Id);

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

        public bool MarcaYaExiste(string nombreMarcaNueva) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Descripcion
                                          FROM MARCAS
                                          WHERE Descripcion COLLATE Latin1_General_CI_AI LIKE @nombreMarcaNueva OR
                                          @nombreMarcaNueva COLLATE Latin1_General_CI_AI LIKE CONCAT('%', Descripcion, '%')");

                accesoDatos.AgregarParametro("nombreMarcaNueva", nombreMarcaNueva);

                return (string) accesoDatos.EjecutarScalar() != null;
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

        public Marca ObtenerMarcaPorId(int id) 
        {
            Marca marcaBuscada = null;
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM MARCAS
                                          WHERE Id = @id");

                accesoDatos.AgregarParametro("id", id);

                accesoDatos.EjecutarLector();

                if (accesoDatos.Lector.Read())
                {
                    marcaBuscada = new Marca();

                    marcaBuscada.Id = accesoDatos.Lector.GetInt32(0);
                    marcaBuscada.Descripcion = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                }

                return marcaBuscada;
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

        public List<Marca> ObtenerMarcas() 
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM MARCAS");

                accesoDatos.EjecutarLector();

                while(accesoDatos.Lector.Read()) 
                {
                    Marca marca = new Marca();

                    marca.Id = accesoDatos.Lector.GetInt32(0);
                    marca.Descripcion = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);

                    listaMarcas.Add(marca);
                }

                return listaMarcas;
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

        public List<Marca> ObtenerMarcas(string tipoOrden = null)
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM MARCAS");

                if (!string.IsNullOrEmpty(tipoOrden))
                {
                    accesoDatos.ConcatenarQuery($" {tipoOrden}");
                }

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    Marca marca = new Marca();

                    marca.Id = accesoDatos.Lector.GetInt32(0);
                    marca.Descripcion = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);

                    listaMarcas.Add(marca);
                }

                return listaMarcas;
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
