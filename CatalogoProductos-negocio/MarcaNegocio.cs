using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public class MarcaNegocio
    {
        public List<Marca> ObtenerMarcas() 
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, Descripcion
                                          FROM Marcas");

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
    }
}
