using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FerreteriaPro1.Negocio
{
    public class Ferreteria
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
        private string _IdTipoFerreteria;
        public string IdTipoFerreteria
        {
            get
            {
                return _IdTipoFerreteria;
            }
            set
            {
                _IdTipoFerreteria = value;
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
        private bool ValidarDatosFerreteria()
        {
            bool _Resultado = true;
            try
            {
                if (_Nombres == "")
                {
                    _Mensaje += "Debe ingresar su nombre"; _Resultado = false;
                }
                if (_IdTipoFerreteria == "")
                {
                    _Mensaje += "Debe ingresar el tipo de ferreteria"; _Resultado = false;
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
        public bool OperarFerreteria()
        {
            bool _Resultado = true;
            try
            {
                _Resultado = ValidarDatosFerreteria();
                if (_Resultado)
                {
                    FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                    string consulta = "insert into ferreteria values ("
                        + "'" + _Nombres+ "', "
                        + _Telefono + ","
                         + _IdTipoFerreteria
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