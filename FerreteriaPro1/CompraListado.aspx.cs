using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class CompraListado : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad();
                ObtenerCompras();
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
        private void ObtenerCompras()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtArticulos = new DataTable();
                    dtArticulos = _Conexion.CargarDatos("select com.id_compra,com.fecha_compra,pro.nombre_proveedor,sum(cod.total_compra_detalle) as monto_total_compra from compra com inner join compra_detalle cod on com.id_compra = cod.id_compra inner join PROVEEDOR pro on com.id_proveedor=pro.id_proveedor group by com.id_compra,com.fecha_compra,pro.nombre_proveedor");
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