using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Proveedor
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
        private string _NombreProveedor;
        public string NombreProveedor
        {
            get
            {
                return _NombreProveedor;
            }
            set
            {
                _NombreProveedor = value;
            }
        }
        private string _TelefonoProveedor;
        public string TelefonoProveedor
        {
            get
            {
                return _TelefonoProveedor;
            }
            set
            {
                _TelefonoProveedor = value;
            }
        }
        private string _DireccionProveedor;
        public string DireccionProveedor
        {
            get
            {
                return _DireccionProveedor;
            }
            set
            {
                _DireccionProveedor = value;
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
        private bool ValidarDatosProveedor()
        {
            bool _Resultado = true;
            try
            {
                if (_NombreProveedor == "")
                {
                    _Mensaje += "Debe ingresar el nombre del proveedor"; _Resultado = false;
                }
                if (_DireccionProveedor == "")
                {
                    _Mensaje += "Debe ingresar la direccion del proveedor"; _Resultado = false;
                }
                if (_TelefonoProveedor != "")
                {
                    int telefono = 0;
                    if (int.TryParse(_TelefonoProveedor, out telefono))
                    {
                        if (_TelefonoProveedor.Length != 8)
                        {
                            _Mensaje += "Debe ingresar un numero de telefono con 8 digitos"; _Resultado = false;
                        }
                    }
                    else
                    {
                        _Mensaje += "Debe ingresar un numero de telefono valido"; _Resultado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        public bool OperarProveedor()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatosProveedor();
                if (_Resultado)
                {
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    string consulta = "insert into proveedor values ("
                        + "'" + _NombreProveedor + "', "
                        + "'" + _DireccionProveedor + "', "
                        + _TelefonoProveedor + ""
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