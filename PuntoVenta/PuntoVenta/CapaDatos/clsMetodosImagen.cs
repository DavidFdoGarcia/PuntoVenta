using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoVenta.CapaDatos
{
    class clsMetodosImagen
    {
        private ConexionBD Conexion = new ConexionBD();
        private SqlCommand Comando = new SqlCommand();
        private SqlDataReader LeerFilas;

        public DataTable llenarDataArticulo()
        {
            DataTable Tabla = new DataTable();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "sp_llenarDataArticulo";
            Comando.CommandType = CommandType.StoredProcedure;
            LeerFilas = Comando.ExecuteReader();
            Tabla.Load(LeerFilas);
            LeerFilas.Close();
            Comando.Connection = Conexion.CerrarConexion();
            return Tabla;
            
        }

        public DataTable llenarDataBuscar(string busco)
        {
            DataTable Tabla = new DataTable();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "sp_BuscarArticulo";
            Comando.Parameters.AddWithValue("@Buscar", busco);
            Comando.CommandType = CommandType.StoredProcedure;
            LeerFilas = Comando.ExecuteReader();
            Tabla.Load(LeerFilas);
            LeerFilas.Close();
            Comando.Connection = Conexion.CerrarConexion();
            return Tabla;

        }
    }
}
