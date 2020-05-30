using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Compra
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
        private string _IdProveedor;
        public string IdProveedor
        {
            get
            {
                return _IdProveedor;
            }
            set
            {
                _IdProveedor = value;
            }
        }
        private DataTable _dtDetalleCompra;
        public DataTable dtDetalleCompra
        {
            get
            {
                return _dtDetalleCompra;
            }
            set
            {
                _dtDetalleCompra = value;
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
                if (_dtDetalleCompra.Rows.Count == 0)
                {
                    _Mensaje += "Debe ingresar al menos un artíulo"; _Resultado = false;
                }
                if (_IdProveedor == "")
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
        public bool OperarCompra()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatos();
                if (_Resultado)
                {
                    int _IdCompra = 0;
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    if (_Conexion.conectar())
                    {
                        DataTable dtDatos = new DataTable();
                        dtDatos = _Conexion.CargarDatos("select isnull(max(id_compra),0) + 1 from compra");
                        if (dtDatos.Rows.Count > 0)
                        {
                            _IdCompra = int.Parse(dtDatos.Rows[0][0].ToString());
                        }
                        string consulta = "insert into compra values ("
                            + "" + _IdCompra + ", "
                    + "" + _IdProveedor + ", "
                    + "getdate()"
                   + ")";
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            foreach (DataRow _Detalle in dtDetalleCompra.Rows)
                            {
                                consulta = "insert into compra_detalle values ("
                            + "" + _IdCompra + ", "
                            + "" + _Detalle["id_articulo"] + ", "
                            + "" + _Detalle["cantidad_articulo"] + ", "
                            + "" + float.Parse(_Detalle["cantidad_articulo"].ToString()) * float.Parse(_Detalle["precio_articulo"].ToString())
                           + ")";
                                // actualizar stock en tabla ARTICULOS
                                if (_Conexion.EjecutarComandoSql(consulta) > 0)
                                {
                                    consulta = "update articulos set stock_articulo = stock_articulo + " + _Detalle["cantidad_articulo"] + " where id_articulo = " + _Detalle["id_articulo"];
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