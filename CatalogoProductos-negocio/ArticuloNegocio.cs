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
        public List<Articulo> ObtenerArticulos() 
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

                accesoDatos.EjecutarQuery();
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
    }
}
