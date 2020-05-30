using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class FerreteriaListado : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad();
                ObtenerFerreteria();
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
                if (Request.Cookies["idUsuario"] != null)
                {
                    if (Request.Cookies["idUsuario"].Value == null || Request.Cookies["idUsuario"].Value == "")
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
        private void ObtenerFerreteria()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtFerreteria = new DataTable();
                    dtFerreteria = _Conexion.CargarDatos("select * from ferreteria fer inner join TIPO_FERRETERIA tip on fer.id_tipoferreteria=tip.id_tipoferreteria");
                    if (dtFerreteria.Rows.Count > 0)
                    {
                        dgvListado.DataSource = dtFerreteria;
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