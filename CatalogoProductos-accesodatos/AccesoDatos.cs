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

        public SqlDataReader Lector { get => _lector;}

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

        public void EjecutarLector() 
        {
            _lector = _comando.ExecuteReader();
        }

        public void EjecutarQuery() 
        {
            _conexion.Open();

            _comando.ExecuteNonQuery();
        }

        public void EjecutarScalar() 
        {
            _conexion.Open();

            _comando.ExecuteScalar();
        }

        public void CerrarConexion() 
        {
            _conexion.Close();
        }
    }
}
