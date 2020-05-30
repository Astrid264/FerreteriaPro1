using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Clientes : System.Web.UI.Page
    {
        public string _MensajeError = "";
        public string _MensajeSatisfactorio = "";
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _Operacion = "";
        public string _TipoOperacion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["idusuario"] != null)
            {
                if (Request.Cookies["idusuario"].Value == null || Request.Cookies["idusuario"].Value == "")
                {
                    Response.Redirect("login.aspx");
                }
            }
            else
                Response.Redirect("login.aspx");

            ObtenerDatosCliente();
        }
        private void ObtenerDatosCliente()
        {
            try
            {
                string idCliente = Request.QueryString["idc"];
                _TipoOperacion = Request.QueryString["op"];

                if (_TipoOperacion == null || _TipoOperacion == "")
                {
                    _MensajeError = "Debe ingresar operacion";
                }
                else
                {
                    if (_TipoOperacion != "1")
                    {
                        if (idCliente == null || idCliente == "")
                        {
                            _MensajeError = "Debe ingresar id del cliente";
                        }
                        else
                        {
                            if (_TipoOperacion == "2")
                                _Operacion = "Modificar";
                            if (_TipoOperacion == "3")
                                _Operacion = "Eliminar";
                            if (_TipoOperacion == "4")
                            {
                                _Operacion = "Consultar";
                                btnGuardar.Enabled = false;
                            }
                            if (!IsPostBack)
                            {
                                if (_Conexion.conectar())
                                {
                                    DataTable dtClientes = new DataTable();
                                    dtClientes = _Conexion.CargarDatos("select * from clientes where id_cliente = " + idCliente);
                                    foreach (DataRow _Cliente in dtClientes.Rows)
                                    {
                                        txtIdCliente.Text = _Cliente["id_cliente"].ToString();
                                        txtNombreCliente.Text = _Cliente["nombre_cliente"].ToString();
                                        txtDireccionClientes.Text = _Cliente["direccion_cliente"].ToString();
                                        txtTelefonoClientes.Text = _Cliente["telefono_cliente"].ToString();
                                    }
                                }
                                else
                                {
                                    _MensajeError = _Conexion.Mensaje;
                                }
                            }
                        }
                    }
                    else if (_TipoOperacion == "1")
                    {
                        _Operacion = "Agregar";
                    }
                    btnGuardar.Text = _Operacion;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Negocio.Clientes _Clientes = new Negocio.Clientes();
                _Clientes.IdCliente = txtIdCliente.Text.Trim();
                _Clientes.NombreCliente = txtNombreCliente.Text.Trim();
                _Clientes.DireccionCliente = txtDireccionClientes.Text.Trim();
                _Clientes.TelefonoCliente = txtTelefonoClientes.Text.Trim();
                _Clientes.TipoOperacion = _TipoOperacion;
                if (_Clientes.OperarClientes())
                {

                    _MensajeSatisfactorio = "Operación realizada correctamente";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar el cliente: " + _Clientes.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
    }

}