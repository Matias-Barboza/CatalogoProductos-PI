using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public class ArticuloNegocio
    {
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
        
        public List<Articulo> ObtenerArticulosRandom(int cantidadArticulosSolicitados, int idProductoExcluido) 
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
    }
}
