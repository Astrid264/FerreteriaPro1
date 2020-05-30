using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Proveedores : System.Web.UI.Page
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

            ObtenerDatosProveedor();
        }
        private void ObtenerDatosProveedor()
        {
            try
            {
                if (!IsPostBack)
                {
                    string idProveedor = Request.QueryString["idp"];
                    string operacion = Request.QueryString["op"];
                    if (operacion == null || operacion == "" || idProveedor == null || idProveedor == "")
                    {
                        //_MensajeError = "Debe ingresar operacion y codigo del proveedor";
                    }
                    else
                    {
                        if (operacion == "4")
                        {
                            if (_Conexion.conectar())
                            {
                                DataTable dtProveedor = new DataTable();
                                dtProveedor = _Conexion.CargarDatos("select * from proveedor where id_proveedor = " + idProveedor);
                                foreach (DataRow _FilaProveedor in dtProveedor.Rows)
                                {
                                    txtNombreProveedor.Text = _FilaProveedor["nombre_proveedor"].ToString();
                                    txtTelefonoProveedor.Text = _FilaProveedor["telefono_proveedor"].ToString();
                                    txtDireccionProveedor.Text = _FilaProveedor["direccion_proveedor"].ToString();
                                }

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

            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Negocio.Proveedor _Proveedor = new Negocio.Proveedor();
                _Proveedor.NombreProveedor = txtNombreProveedor.Text.Trim();
                _Proveedor.TelefonoProveedor = txtTelefonoProveedor.Text.Trim();
                _Proveedor.DireccionProveedor = txtDireccionProveedor.Text.Trim();
                if (_Proveedor.OperarProveedor())
                {

                    _MensajeSatisfactorio = "Proveedor agregado correctamente";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar el proveedor: " + _Proveedor.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
    }
}