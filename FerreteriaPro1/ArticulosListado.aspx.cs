using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class ArticulosListado : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Seguridad();
                ObtenerArticulos();
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
        private void ObtenerArticulos()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtArticulos = new DataTable();
                    dtArticulos = _Conexion.CargarDatos("select * from articulos fer inner join PROVEEDOR pro on fer.id_proveedor=pro.id_proveedor inner join unidad_medida ume on fer.id_unidad_medida = ume.id_unidad_medida");
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