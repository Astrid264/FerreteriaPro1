using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Articulos
    {
        #region "Propiedades"
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
        private string _NombreArticulo;
        public string NombreArticulo
        {
            get
            {
                return _NombreArticulo;
            }
            set
            {
                _NombreArticulo = value;
            }
        }
        private string _DescripcionArticulo;
        public string DescripcionArticulo
        {
            get
            {
                return _DescripcionArticulo;
            }
            set
            {
                _DescripcionArticulo = value;
            }
        }
        private string _IdUnidadMedida;
        public string IdUnidadMedida
        {
            get
            {
                return _IdUnidadMedida;
            }
            set
            {
                _IdUnidadMedida= value;
            }
        }
        private string _PrecioArticulo;
        public string PrecioArticulo
        {
            get
            {
                return _PrecioArticulo;
            }
            set
            {
                _PrecioArticulo = value;
            }
        }
        private string _StockArticulo;
        public string StockArticulo
        {
            get
            {
                return _StockArticulo;
            }
            set
            {
                _StockArticulo = value;
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
        private int _TipoOperacion;
        public int TipoOperacion
        {
            get
            {
                return _TipoOperacion;
            }
            set
            {
                _TipoOperacion = value;
            }
        }
        #endregion
        #region "Métodos"
        private bool ValidarDatos()
        {
            bool _Resultado = true;
            try
            {
                if (_NombreArticulo == "")
                {
                    _Mensaje += "Debe ingresar el nombre del articulo"; _Resultado = false;
                }
                if (_DescripcionArticulo == "")
                {
                    _Mensaje += "Debe ingresar la descripcion del articulo"; _Resultado = false;
                }
                if (_DescripcionArticulo == "")
                {
                    _Mensaje += "Debe ingresar la unidad del articulo"; _Resultado = false;
                }
                if(_PrecioArticulo== "")
                {
                    _Mensaje += "Debe ingresar el precio del articulo"; _Resultado = false;
                }
                if (_StockArticulo == "")
                {
                    _Mensaje += "Debe ingresar el stock del articulo"; _Resultado = false;
                }
                if (_IdProveedor == "")
                {
                    _Mensaje += "Debe ingresar el proveedor"; _Resultado = false;
                }
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        public bool OperarArticulos()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatos();
                if (_Resultado)
                {
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    string consulta = "insert into articulos values ("
                        + "'" + _NombreArticulo + "', "
                        + "'" + _DescripcionArticulo + "', "
                        + "" + _IdUnidadMedida + ", "
                        + "'" + _PrecioArticulo + "', "
                        + "'" + _StockArticulo + "',"
                        + "" + _IdProveedor + ""
                       + ")";
                    if (_Conexion.EjecutarComandoSql(consulta) > 0)
                    {
                        _Resultado = true;
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
