using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace FerreteriaPro1.conexion
{
    public class conexion
    {
        private SqlConnection _conexion;
        private SqlCommand _comando;
        private string cadena = "";
        public string Mensaje = "";
        public bool conectar()
        {
            try
            {
                cadena = @"Data Source = localhost\sqlexpress; Initial Catalog = Ferreteria proyecto 1; Integrated Security=True;";
                _conexion = new SqlConnection(cadena);
                if (_conexion.State != ConnectionState.Open)
                {
                    _conexion.Open();
                }

            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return false;
            }
            return true;
        }
        public bool desconectar()
        {
            try
            {
                _conexion.Close();
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return false;
            }
            return true;
        }
        public DataTable CargarDatos(string consulta)
        {
            try
            {
                DataTable dtDatos = new DataTable();
                SqlCommand comandos = new SqlCommand(consulta, _conexion);
                SqlDataReader rdr = comandos.ExecuteReader();
                dtDatos.Load(rdr);
                rdr.Close();
                rdr.Dispose();
                desconectar();
                return dtDatos;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return null;
            }
        }

        public int EjecutarComandoSql(string consulta)
        {
            try
            {
                conectar();
                _comando = new SqlCommand(consulta, _conexion);               
                int resultado = _comando.ExecuteNonQuery();
                desconectar();
                return resultado;
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                return -1;
            }
        }

    }

}