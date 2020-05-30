using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Clientes
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
        private string _NombreCliente;
        public string NombreCliente
        {
            get
            {
                return _NombreCliente;
            }
            set
            {
                _NombreCliente = value;
            }
        }
        private string _TelefonoCliente;
        public string TelefonoCliente
        {
            get
            {
                return _TelefonoCliente;
            }
            set
            {
                _TelefonoCliente = value;
            }
        }
        private string _DireccionCliente;
        public string DireccionCliente
        {
            get
            {
                return _DireccionCliente;
            }
            set
            {
                _DireccionCliente = value;
            }
        }
        private string _TipoOperacion;
        public string TipoOperacion
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
        private bool ValidarDatosClientes()
        {
            bool _Resultado = true;
            try
            {
                if (_TipoOperacion != "1") // Es modificar o eliminar
                {
                    if (_IdCliente == "")
                    {
                        _Mensaje += "Debe ingresar el id del cliente"; _Resultado = false;
                    }
                }

                if (_NombreCliente == "")
                {
                    _Mensaje += "Debe ingresar el nombre del cliente"; _Resultado = false;
                }
                if (_DireccionCliente == "")
                {
                    _Mensaje += "Debe ingresar la direccion del cliente"; _Resultado = false;
                }

                int telefono = 0;
                if (int.TryParse(_TelefonoCliente, out telefono))
                {
                    if (_TelefonoCliente.Length != 8)
                    {
                        _Mensaje += "Debe ingresar un numero de telefono con 8 digitos"; _Resultado = false;
                    }
                }
                else
                {
                    _Mensaje += "Debe ingresar un numero de telefono valido"; _Resultado = false;
                }

            }
            catch (Exception ex)
            {
                _Mensaje = ex.Message;
                _Resultado = false;
            }
            return _Resultado;
        }
        public bool OperarClientes()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatosClientes();
                if (_Resultado)
                {
                    if (_TipoOperacion == "1")
                    {
                        FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                        string consulta = "insert into clientes (nombre_cliente, direccion_cliente, telefono_cliente) values ("
                            + "'" + _NombreCliente + "', "
                            + "'" + _DireccionCliente + "', "
                            + _TelefonoCliente + ""
                           + ")";
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            _Resultado = true;
                        }
                        else
                        {
                            _Resultado = false;
                            _Mensaje = _Conexion.Mensaje;
                        }
                    }
                    if (_TipoOperacion == "2")
                    {
                        FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                        string consulta = "update clientes set nombre_cliente = '" + _NombreCliente + "', direccion_cliente = '" + _DireccionCliente + "', telefono_cliente= " + _TelefonoCliente + " where id_cliente = " + _IdCliente;
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            _Resultado = true;
                        }
                        else
                        {
                            _Resultado = false;
                            _Mensaje = _Conexion.Mensaje;
                        }
                    }
                    if (_TipoOperacion == "3")
                    {
                        FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                        string consulta = "delete from clientes where id_cliente = " + _IdCliente;
                        if (_Conexion.EjecutarComandoSql(consulta) > 0)
                        {
                            _Resultado = true;
                        }
                        else
                        {
                            _Resultado = false;
                            _Mensaje = _Conexion.Mensaje;
                        }
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
        #endregion 
    }
}