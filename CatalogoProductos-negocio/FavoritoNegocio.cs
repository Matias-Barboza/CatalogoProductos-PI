using CatalogoProductos_dominio;
using CatalogoProductos_utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_negocio
{
    public class FavoritoNegocio
    {
        public bool AñadirFavorito(Favorito favorito) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            int idNuevoFavorito;

            try
            {
                accesoDatos.SetearQuery(@"INSERT INTO FAVORITOS(IdUser, IdArticulo)
                                          OUTPUT INSERTED.Id
                                          VALUES(@idUsuario, @idArticulo)");

                accesoDatos.AgregarParametro("idUsuario", favorito.IdUsuario);
                accesoDatos.AgregarParametro("idArticulo", favorito.IdArticulo);

                idNuevoFavorito = (int)accesoDatos.EjecutarScalar();

                return idNuevoFavorito != 0;
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

        public bool EliminarFavorito(Favorito favorito)
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"DELETE FROM FAVORITOS WHERE IdUser = @idUsuario AND IdArticulo = @idArticulo");

                accesoDatos.AgregarParametro("idUsuario", favorito.IdUsuario);
                accesoDatos.AgregarParametro("idArticulo", favorito.IdArticulo);

                return accesoDatos.EjecutarQuery() == 1;
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

        public bool ExisteFavorito(Favorito favorito) 
        {
            AccesoDatos accesoDatos = new AccesoDatos();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, IdUser, IdArticulo
                                          FROM FAVORITOS
                                          WHERE IdUser = @idUsuario AND IdArticulo = @idArticulo");

                accesoDatos.AgregarParametro("idUsuario", favorito.IdUsuario);
                accesoDatos.AgregarParametro("idArticulo", favorito.IdArticulo);

                return accesoDatos.EjecutarScalar() != null;
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

        public List<Favorito> ObtenerFavoritosUsuario(int idUsuario)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Favorito> listaFavoritos = new List<Favorito>();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, IdUser, IdArticulo
                                          FROM FAVORITOS
                                          WHERE IdUser = @idUsuario");

                accesoDatos.AgregarParametro("idUsuario", idUsuario);

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read()) 
                {
                    Favorito favorito = new Favorito();

                    favorito.Id = accesoDatos.Lector.GetInt32(0);
                    favorito.IdUsuario = accesoDatos.Lector.GetInt32(1);
                    favorito.IdArticulo = accesoDatos.Lector.GetInt32(2);

                    listaFavoritos.Add(favorito);
                }

                return listaFavoritos;
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

        public List<int> ObtenerIdArticulosFavoritosUsuario(int idUsuario)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<int> listaIdArticulosFavoritos = new List<int>();

            try
            {
                accesoDatos.SetearQuery(@"SELECT Id, IdUser, IdArticulo
                                          FROM FAVORITOS
                                          WHERE IdUser = @idUsuario");

                accesoDatos.AgregarParametro("idUsuario", idUsuario);

                accesoDatos.EjecutarLector();

                while (accesoDatos.Lector.Read())
                {
                    listaIdArticulosFavoritos.Add(accesoDatos.Lector.GetInt32(2));
                }

                return listaIdArticulosFavoritos;
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
