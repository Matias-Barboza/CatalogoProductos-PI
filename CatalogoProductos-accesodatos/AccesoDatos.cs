using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoProductos_utilidades
{
    public class AccesoDatos
    {
        private SqlConnection _conexion;
        private SqlCommand _comando;
        private SqlDataReader _lector;

        public SqlDataReader Lector { get => _lector; }

        public AccesoDatos()
        {
            _conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CATALAGO_WEB_DB"].ConnectionString);
            _comando = new SqlCommand();
            _comando.Connection = _conexion;
        }

        public void SetearQuery(string query)
        {
            _comando.CommandText = query;
        }

        public void ConcatenarQuery(string extensionQuery) 
        {
            _comando.CommandText += extensionQuery;
        }

        public void AgregarParametro(string nombre, object valor)
        {
            _comando.Parameters.AddWithValue(nombre, valor);
        }

        public void EjecutarLector()
        {
            try
            {
                _conexion.Open();
                _lector = _comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarQuery()
        {
            try
            {
                _conexion.Open();
                _comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarScalar()
        {
            try
            {
                _conexion.Open();
                _comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CerrarConexion()
        {
            _conexion.Close();
        }
    }
}
