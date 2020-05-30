using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Usuario
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
        private string _IdUsuario;
        public string IdUsuario
        {
            get
            {
                return _IdUsuario;
            }
            set
            {
                _IdUsuario = value;
            }
        }
        private string _Usuario;
        public string NombreUsuario
        {
            get
            {
                return _Usuario;
            }
            set
            {
                _Usuario = value;
            }
        }
        private string _Telefono;
        public string Telefono
        {
            get
            {
                return _Telefono;
            }
            set
            {
                _Telefono = value;
            }
        }
        private string _Contraseña;
        public string Contraseña
        {
            get
            {
                return _Contraseña;
            }
            set
            {
                _Contraseña = value;
            }
        }
        private string _IdRol;
        public string IdRol
        {
            get
            {
                return _IdRol;
            }
            set
            {
                _IdRol = value;
            }
        }
        private string _Nombres;
        public string Nombres
        {
            get
            {
                return _Nombres;
            }
            set
            {
                _Nombres = value;
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
        private bool ValidarDatosUsuario()
        {
            bool _Resultado = true;
            try
            {
                if (_Usuario == "")
                {
                    _Mensaje += "Debe ingresar su usuario"; _Resultado = false;
                }
                if (_Contraseña == "")
                {
                    _Mensaje += "Debe ingresar su contraseña"; _Resultado = false;
                }
                if (_IdRol == "")
                {
                    _Mensaje += "Debe ingresar su rol"; _Resultado = false;
                }
                if (_Nombres == "")
                {
                    _Mensaje += "Debe ingresar su nombre"; _Resultado = false;
                }
                if (_Telefono != "")
                {
                    int telefono = 0;
                    if (int.TryParse(_Telefono, out telefono))
                    {
                        if (_Telefono.Length != 8)
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
        public bool OperarUsuario()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatosUsuario();
                if (_Resultado)
                {
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    string consulta = "insert into usuario values ("
                        + "'" + _Usuario + "', "
                        + "'" + _Contraseña + "', "
                        + "'" + _Nombres + "', "
                        + _Telefono + ","
                         + _IdRol
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