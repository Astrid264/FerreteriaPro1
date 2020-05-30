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
                if (!IsPostBack)
                {
                    string idCliente = Request.QueryString["idc"];
                    string operacion = Request.QueryString["op"];
                    if (operacion == null || operacion == "" || idCliente == null || idCliente == "")
                    {
                        _MensajeError = "Debe ingresar operacion y codigo de cliente";
                    }
                    else
                    {
                        if (operacion == "4")
                        {
                            if (_Conexion.conectar())
                            {
                                DataTable dtClientes = new DataTable();
                                dtClientes = _Conexion.CargarDatos("select * from clientes where id_cliente = " + idCliente);
                                foreach (DataRow _Cliente in dtClientes.Rows)
                                {
                                    txtNombreCliente.Text = _Cliente["nombre_cliente"].ToString();
                                    txtDireccionClientes.Text = _Cliente["direccion_cliente"].ToString();
                                    txtTelefonoClientes.Text = _Cliente["telefono_cliente"].ToString();
                                }
                                btnGuardar.Enabled = false;
                            }
                            else
                            {
                                _MensajeError = _Conexion.Mensaje;
                            }
                        }
                    }
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
                _Clientes.NombreCliente = txtNombreCliente.Text.Trim();
                _Clientes.DireccionCliente = txtDireccionClientes.Text.Trim();
                _Clientes.TelefonoCliente = txtTelefonoClientes.Text.Trim();
                if (_Clientes.OperarClientes())
                {

                    _MensajeSatisfactorio = "Cliente agregado correctamente";
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