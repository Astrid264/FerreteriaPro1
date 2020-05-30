using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Abastecimiento
    {
        #region "Propiedades"
        private string _IdArticulo;
        public string IdArticulo
        {
            get
            {
                return _IdArticulo;
            }
            set
            {
                _IdArticulo = value;
            }
        }
        private string _Cantidad;
        public string Cantidad
        {
            get
            {
                return _Cantidad;
            }
            set
            {
                _Cantidad = value;
            }
        }
        private string _IdFerreteria;
        public string IdFerreteria
        {
            get
            {
                return _IdFerreteria;
            }
            set
            {
                _IdFerreteria = value;
            }
        }
        private string _Mensaje;
        public string Mensaje
        {
            get
            {
                return _Mensaje;
            }
            set
            {
                _Mensaje = value;
            }
        }
        #endregion
        #region "Métodos"
        private bool ValidarDatos()
        {
            bool _Resultado = true;
            try
            {
                if (_IdArticulo == "")
                {
                    _Mensaje += "Debe ingresar el articulo"; _Resultado = false;
                }
                if (_Cantidad == "")
                {
                    _Mensaje += "Debe ingresar la cantidad"; _Resultado = false;
                }
                else
                {
                    int cantidad = 0;
                    if (!int.TryParse(_Cantidad, out cantidad))
                    {
                        _Mensaje += "Debe ingresar la cantidad con valores numéricos"; _Resultado = false;
                    }
                    else
                    {
                        FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                        if (_Conexion.conectar())
                        {
                            DataTable dtDatos = new DataTable();
                            dtDatos = _Conexion.CargarDatos("select stock_articulo from articulos where id_articulo = " + _IdArticulo);
                            if (dtDatos.Rows.Count > 0)
                            {
                                int stockArticulo = int.Parse(dtDatos.Rows[0][0].ToString());
                                if (int.Parse(_Cantidad) > stockArticulo)
                                {
                                    _Mensaje += "La cantidad ingresada es mayor al stock disponible del artículo."; _Resultado = false;
                                }
                            }
                        }                        
                    }
                }
                if (_IdFerreteria == "")
                {
                    _Mensaje += "Debe ingresar la ferretería"; _Resultado = false;
                }
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        public bool OperarAbastecimiento()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatos();
                if (_Resultado)
                {
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    string consulta = "insert into articulo_ferreteria values ("
                        + "" + _IdArticulo + ", "
                        + "" + _Cantidad + ", "
                        + "" + _IdFerreteria
                       + ")";
                    if (_Conexion.EjecutarComandoSql(consulta) > 0)
                    {
                        // Actualizar stock en tabla articulos
                        _Resultado = true;
                        _Conexion = new FerreteriaPro1.conexion.conexion();
                        consulta = "update articulos set stock_articulo = stock_articulo - " + _Cantidad + " where id_articulo = " + _IdArticulo;
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            _Resultado = true;
                        }
                        else
                        {
                            _Resultado = false;
                        }
                    }
                    else
                    {
                        _Resultado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        #endregion
    }
}