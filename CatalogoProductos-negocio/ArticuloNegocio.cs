using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public enum EstadoOperacion
    {
        AÑADIDO_EXITOSO = 1,
        AÑADIDO_FALLIDO = 2,
        EDICION_EXITOSA = 3,
        EDICION_FALLIDA = 4,
        ELIMINACION_EXITOSA = 5,
        ELIMINACION_FALLIDA = 6
    }

    public class ArticuloNegocio
    {
        public const string PLACEHOLDER_IMAGEN_ARTICULO = "https://pngimg.com/uploads/box/box_PNG137.png";
        public const string PLACEHOLDER_IMAGEN = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";

        public bool AñadirArticulo(Articulo articulo, out int idNuevoArticulo) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            idNuevoArticulo = 0;

            try 
            {
                accesoDatos.SetearQuery(@"INSERT INTO ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)
                                          OUTPUT INSERTED.id
                                          VALUES (@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @imagenUrl, @precio)");

                accesoDatos.AgregarParametro("codigo", articulo.CodigoArticulo);
                accesoDatos.AgregarParametro("nombre", articulo.Nombre);
                accesoDatos.AgregarParametro("descripcion", articulo.Descripcion);
                accesoDatos.AgregarParametro("idMarca", articulo.Marca.Id);
                accesoDatos.AgregarParametro("idCategoria", articulo.Categoria.Id);
                accesoDatos.AgregarParametro("imagenUrl", articulo.ImagenUrl ?? (object) DBNull.Value);
                accesoDatos.AgregarParametro("precio", articulo.Precio);

                idNuevoArticulo = (int) accesoDatos.EjecutarScalar();

                return idNuevoArticulo != 0;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally 
            {
                accesoDatos.CerrarConexion();
                accesoDatos = null;
            }
        }

        public bool ActualizarArticulo(Articulo articulo) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"UPDATE ARTICULOS SET 
                                                              Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion,
                                                              IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio
                                                           WHERE Id = @id");

                accesoDatos.AgregarParametro("codigo", articulo.CodigoArticulo);
                accesoDatos.AgregarParametro("nombre", articulo.Nombre);
                accesoDatos.AgregarParametro("descripcion", articulo.Descripcion);
                accesoDatos.AgregarParametro("idMarca", articulo.Marca.Id);
                accesoDatos.AgregarParametro("idCategoria", articulo.Categoria.Id);
                accesoDatos.AgregarParametro("imagenUrl", articulo.ImagenUrl);
                accesoDatos.AgregarParametro("precio", articulo.Precio);
                accesoDatos.AgregarParametro("id", articulo.Id);

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

        public bool EliminarArticulo(int idArticulo) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"DELETE FROM ARTICULOS WHERE Id = @id");
                accesoDatos.AgregarParametro("id", idArticulo);

                return accesoDatos.EjecutarQuery() == 1;
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

        // ----------------------------------------------- RECUPERAR ------------------------------------------------------------------------------------------
        public Articulo ObtenerArticuloPorId(int id) 
        {
            Articulo articuloBuscado = null;
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id, m.Descripcion,c.Id, c.Descripcion, a.ImagenUrl, a.Precio
                                          FROM ARTICULOS AS a
                                          INNER JOIN MARCAS AS m
	                                        ON a.IdMarca = m.Id
                                          INNER JOIN CATEGORIAS AS c
	                                        ON a.IdCategoria = c.Id
                                          WHERE a.Id = @id_articulo_buscado");

                accesoDatos.AgregarParametro("id_articulo_buscado", id);

                accesoDatos.EjecutarLector();

                if (accesoDatos.Lector.Read()) 
                {
                    articuloBuscado = new Articulo();

                    articuloBuscado.Id = accesoDatos.Lector.GetInt32(0);
                    articuloBuscado.CodigoArticulo = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                    articuloBuscado.Nombre = accesoDatos.Lector.IsDBNull(2) ? "" : accesoDatos.Lector.GetString(2);
                    articuloBuscado.Descripcion = accesoDatos.Lector.IsDBNull(3) ? "" : accesoDatos.Lector.GetString(3);
                    articuloBuscado.Marca.Id = accesoDatos.Lector.IsDBNull(4) ? -1 : accesoDatos.Lector.GetInt32(4);
                    articuloBuscado.Marca.Descripcion = accesoDatos.Lector.IsDBNull(5) ? "" : accesoDatos.Lector.GetString(5);
                    articuloBuscado.Categoria.Id = accesoDatos.Lector.IsDBNull(6) ? -1 : accesoDatos.Lector.GetInt32(6);
                    articuloBuscado.Categoria.Descripcion = accesoDatos.Lector.IsDBNull(7) ? "" : accesoDatos.Lector.GetString(7);
                    articuloBuscado.ImagenUrl = accesoDatos.Lector.IsDBNull(8) ? "" : accesoDatos.Lector.GetString(8);
                    articuloBuscado.Precio = accesoDatos.Lector.GetDecimal(9);
                }

                return articuloBuscado;
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

        public List<Articulo> ObtenerArticulosPorId(List<int> listaIds)
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id, m.Descripcion,c.Id, c.Descripcion, a.ImagenUrl, a.Precio
                                          FROM ARTICULOS AS a
                                          INNER JOIN MARCAS AS m
	                                        ON a.IdMarca = m.Id
                                          INNER JOIN CATEGORIAS AS c
	                                        ON a.IdCategoria = c.Id
                                          WHERE a.Id IN (SELECT VALUE FROM STRING_SPLIT(@lista_ids,','))");

                string listaIdsStr = string.Join(",", listaIds);

                accesoDatos.AgregarParametro("lista_ids", listaIdsStr);

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = accesoDatos.Lector.GetInt32(0);
                    articulo.CodigoArticulo = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                    articulo.Nombre = accesoDatos.Lector.IsDBNull(2) ? "" : accesoDatos.Lector.GetString(2);
                    articulo.Descripcion = accesoDatos.Lector.IsDBNull(3) ? "" : accesoDatos.Lector.GetString(3);
                    articulo.Marca.Id = accesoDatos.Lector.IsDBNull(4) ? -1 : accesoDatos.Lector.GetInt32(4);
                    articulo.Marca.Descripcion = accesoDatos.Lector.IsDBNull(5) ? "" : accesoDatos.Lector.GetString(5);
                    articulo.Categoria.Id = accesoDatos.Lector.IsDBNull(6) ? -1 : accesoDatos.Lector.GetInt32(6);
                    articulo.Categoria.Descripcion = accesoDatos.Lector.IsDBNull(7) ? "" : accesoDatos.Lector.GetString(7);
                    articulo.ImagenUrl = accesoDatos.Lector.IsDBNull(8) ? "" : accesoDatos.Lector.GetString(8);
                    articulo.Precio = accesoDatos.Lector.GetDecimal(9);

                    listaArticulos.Add(articulo);
                }

                return listaArticulos;
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

        public List<Articulo> ObtenerArticulos(List<string> marcas = null, List<string> categorias = null, string condicionPrecio = "", decimal precio = -1, string tipoOrden = "", string campoBusqueda = "") 
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id, m.Descripcion,c.Id, c.Descripcion, a.ImagenUrl, a.Precio
                                          FROM ARTICULOS AS a
                                          INNER JOIN MARCAS AS m
	                                        ON a.IdMarca = m.Id
                                          INNER JOIN CATEGORIAS AS c
	                                        ON a.IdCategoria = c.Id");

                if (marcas != null) 
                {
                    string strMarcas = string.Join(",", marcas);

                    accesoDatos.ConcatenarQuery(" WHERE m.Descripcion IN (SELECT VALUE FROM STRING_SPLIT(@marcas, ','))");
                    accesoDatos.AgregarParametro("marcas", strMarcas);
                }

                if (categorias != null)
                {
                    string strCategorias = string.Join(",", categorias);

                    accesoDatos.ConcatenarQuery(marcas == null ? " WHERE c.Descripcion IN (SELECT VALUE FROM STRING_SPLIT(@categorias, ','))" :
                                                                 " AND c.descripcion IN (SELECT VALUE FROM STRING_SPLIT(@categorias, ','))");
                    accesoDatos.AgregarParametro("categorias", strCategorias);
                }

                if (condicionPrecio != "" && precio != -1) 
                {
                    accesoDatos.ConcatenarQuery(categorias == null && marcas == null ? $" WHERE a.precio {condicionPrecio} @precio" : $" AND a.precio {condicionPrecio} @precio");
                    accesoDatos.AgregarParametro("precio", precio);
                }

                if (campoBusqueda != "") 
                {
                    accesoDatos.ConcatenarQuery(" WHERE a.Nombre LIKE @busqueda OR m.Descripcion LIKE @busqueda");
                    campoBusqueda = $"%{campoBusqueda}%";
                    accesoDatos.AgregarParametro("busqueda", campoBusqueda);
                }

                if (tipoOrden != "") 
                {
                    accesoDatos.ConcatenarQuery($" {tipoOrden}");
                }

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = accesoDatos.Lector.GetInt32(0);
                    articulo.CodigoArticulo = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                    articulo.Nombre = accesoDatos.Lector.IsDBNull(2) ? "" : accesoDatos.Lector.GetString(2);
                    articulo.Descripcion = accesoDatos.Lector.IsDBNull(3) ? "" : accesoDatos.Lector.GetString(3);
                    articulo.Marca.Id = accesoDatos.Lector.IsDBNull(4) ? -1 : accesoDatos.Lector.GetInt32(4);
                    articulo.Marca.Descripcion = accesoDatos.Lector.IsDBNull(5) ? "" : accesoDatos.Lector.GetString(5);
                    articulo.Categoria.Id = accesoDatos.Lector.IsDBNull(6) ? -1 : accesoDatos.Lector.GetInt32(6);
                    articulo.Categoria.Descripcion = accesoDatos.Lector.IsDBNull(7) ? "" : accesoDatos.Lector.GetString(7);
                    articulo.ImagenUrl = accesoDatos.Lector.IsDBNull(8) ? "" : accesoDatos.Lector.GetString(8);
                    articulo.Precio = accesoDatos.Lector.GetDecimal(9);

                    listaArticulos.Add(articulo);
                }

                return listaArticulos;
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
        
        public List<Articulo> ObtenerArticulosRandom(int cantidadArticulosSolicitados, int idProductoExcluido = 0) 
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos accesoDatos = new AccesoDatos();
            int cantidadArticulosLista;
            int cantidadArticulosABorrar;

            try
            {
                accesoDatos.SetearQuery(@"SELECT a.Id, a.Codigo, a.Nombre, a.Descripcion, m.Id, m.Descripcion,c.Id, c.Descripcion, a.ImagenUrl, a.Precio
                                          FROM ARTICULOS AS a
                                          INNER JOIN MARCAS AS m
	                                        ON a.IdMarca = m.Id
                                          INNER JOIN CATEGORIAS AS c
	                                        ON a.IdCategoria = c.Id");

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read()) 
                {
                    Articulo articulo = new Articulo();

                    articulo.Id = accesoDatos.Lector.GetInt32(0);
                    articulo.CodigoArticulo = accesoDatos.Lector.IsDBNull(1) ? "" : accesoDatos.Lector.GetString(1);
                    articulo.Nombre = accesoDatos.Lector.IsDBNull(2) ? "" : accesoDatos.Lector.GetString(2);
                    articulo.Descripcion = accesoDatos.Lector.IsDBNull(3) ? "" : accesoDatos.Lector.GetString(3);
                    articulo.Marca.Id = accesoDatos.Lector.IsDBNull(4) ? -1 : accesoDatos.Lector.GetInt32(4);
                    articulo.Marca.Descripcion = accesoDatos.Lector.IsDBNull(5) ? "" : accesoDatos.Lector.GetString(5);
                    articulo.Categoria.Id = accesoDatos.Lector.IsDBNull(6) ? -1 : accesoDatos.Lector.GetInt32(6);
                    articulo.Categoria.Descripcion = accesoDatos.Lector.IsDBNull(7) ? "" : accesoDatos.Lector.GetString(7);
                    articulo.ImagenUrl = accesoDatos.Lector.IsDBNull(8) ? "" : accesoDatos.Lector.GetString(8);
                    articulo.Precio = accesoDatos.Lector.GetDecimal(9);

                    listaArticulos.Add(articulo);

                }

                listaArticulos.RemoveAll(x => x.Id == idProductoExcluido);

                cantidadArticulosLista = listaArticulos.Count();

                if (cantidadArticulosLista < cantidadArticulosSolicitados) 
                {
                    return listaArticulos;
                }

                cantidadArticulosABorrar = cantidadArticulosLista - cantidadArticulosSolicitados;

                for (int i = 0; i < cantidadArticulosABorrar; i++) 
                {
                    Random random = new Random();
                    int valorMaximo = listaArticulos.Count() - 1; 

                    listaArticulos.RemoveAt(random.Next(valorMaximo));
                }

                return listaArticulos;
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

        public bool ExisteCodigoProducto(string codigoNuevo) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Codigo FROM ARTICULOS WHERE Codigo LIKE @codigoNuevo");

                accesoDatos.AgregarParametro("codigoNuevo", codigoNuevo);

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

        // ----------------------------------------------- ADICIONALES ----------------------------------------------------------------------------------------

        public static string ConfigurarRutaImagen(string datoRuta) 
        {
            if (datoRuta.StartsWith("https://") || datoRuta.StartsWith("http://")) 
            {
                return datoRuta;
            }

            if (string.IsNullOrEmpty(datoRuta)) 
            {
                return datoRuta;
            }

            return "~/Imagenes/" + datoRuta;
        }

        public static string PrecioFormateado(decimal precioArticulo) 
        {
            string precioSinTratar = precioArticulo.ToString();

            precioSinTratar = precioSinTratar.Substring(0, precioSinTratar.Length - 2);

            decimal precioTratado = decimal.Parse(precioSinTratar);

            return precioTratado.ToString("C");
        }

        public decimal FormatearPrecio(decimal precioArticulo) 
        {
            string precioSinTratar = precioArticulo.ToString();

            precioSinTratar = precioSinTratar.Substring(0, precioSinTratar.Length - 2);

            return decimal.Parse(precioSinTratar);
        }

        public void ConfigurarRutasDeLista(List<Articulo> lista) 
        {
            foreach (Articulo articulo in lista)
            {
                articulo.ImagenUrl = ConfigurarRutaImagen(articulo.ImagenUrl);
            }
        }

        public void ConfigurarPrecios(List<Articulo> lista) 
        {
            foreach (Articulo articulo in lista)
            {
                articulo.Precio = FormatearPrecio(articulo.Precio);
            }
        }
    }
}
