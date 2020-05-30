using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Venta
    {
        #region "Propiedades"        
        private string _CantidadArticulo;
        public string CantidadArticulo
        {
            get
            {
                return _CantidadArticulo;
            }
            set
            {
                _CantidadArticulo = value;
            }
        }
        private string _IdCliente;
        public string IdCliente
        {
            get
            {
                return _IdCliente;
            }
            set
            {
                _IdCliente = value;
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
        private DataTable _dtDetalleVenta;
        public DataTable dtDetalleVenta
        {
            get
            {
                return _dtDetalleVenta;
            }
            set
            {
                _dtDetalleVenta = value;
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
                if (_dtDetalleVenta.Rows.Count == 0)
                {
                    _Mensaje += "Debe ingresar al menos un artíulo"; _Resultado = false;
                }

                if (_IdFerreteria == "")
                {
                    _Mensaje += "Debe ingresar la ferretería"; _Resultado = false;
                }
                if (_IdCliente == "")
                {
                    _Mensaje += "Debe ingresar el cliente"; _Resultado = false;
                }
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        public bool OperarVenta()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatos();
                if (_Resultado)
                {
                    int _IdVenta = 0;
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    if (_Conexion.conectar())
                    {
                        DataTable dtDatos = new DataTable();
                        dtDatos = _Conexion.CargarDatos("select isnull(max(id_venta),0) + 1 from venta");
                        if (dtDatos.Rows.Count > 0)
                        {
                            _IdVenta = int.Parse(dtDatos.Rows[0][0].ToString());
                        }
                        string consulta = "insert into venta values ("
                            + "" + _IdVenta + ", "
                    + "" + _IdCliente + ", "
                    + "" + _IdFerreteria + ", "
                    + "getdate()"
                   + ")";
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            foreach (DataRow _Detalle in dtDetalleVenta.Rows)
                            {
                                consulta = "insert into venta_detalle values ("
                            + "" + _IdVenta + ", "
                            + "" + _Detalle["id_articulo"] + ", "
                            + "" + _Detalle["cantidad_articulo"] + ", "
                            + "" + float.Parse(_Detalle["cantidad_articulo"].ToString()) * float.Parse(_Detalle["precio_articulo"].ToString())
                           + ")";
                                if (_Conexion.EjecutarComandoSql(consulta) > 0)
                                {
                                    consulta = "update articulos set stock_articulo = stock_articulo - " + _Detalle["cantidad_articulo"] + " where id_articulo = " + _Detalle["id_articulo"];
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
                        else
                        {
                            _Resultado = false;
                        }
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