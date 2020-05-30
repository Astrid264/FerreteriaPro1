using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class VentasListado : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad();
                ObtenerVentas();
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
        private void ObtenerVentas()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtArticulos = new DataTable();
                    dtArticulos = _Conexion.CargarDatos("select VEN.ID_VENTA, VEN.FECHA_VENTA, CLI.nombre_cliente, FER.nombre as nombre_ferreteria, SUM(VED.total_venta_detalle) as monto_total_venta from VENTA VEN INNER JOIN CLIENTES CLI ON VEN.id_cliente = CLI.id_cliente INNER JOIN FERRETERIA FER ON FER.id_ferreteria = VEN.ID_FERRETERIA INNER JOIN VENTA_DETALLE VED ON VEN.ID_VENTA = VED.ID_VENTA group by VEN.ID_VENTA , VEN.FECHA_VENTA, CLI.nombre_cliente, FER.nombre");
                    if (dtArticulos.Rows.Count > 0)
                    {
                        dgvListado.DataSource = dtArticulos;
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