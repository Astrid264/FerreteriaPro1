using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class ClientesListado : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad();
                ObtenerClientes();
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        private void Seguridad()
        {
            try
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
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        private void ObtenerClientes()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtUsuario = new DataTable();
                    dtUsuario = _Conexion.CargarDatos("select * from clientes");
                    if (dtUsuario.Rows.Count > 0)
                    {
                        dgvListado.DataSource = dtUsuario;
                        dgvListado.DataBind();
                    }
                }
                else
                {
                    _MensajeError = _Conexion.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
    }
}