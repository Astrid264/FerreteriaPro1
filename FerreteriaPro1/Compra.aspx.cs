using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Compra : System.Web.UI.Page
    {
        DataTable dtDetalleCompra = new DataTable();
        conexion.conexion _Conexion = new conexion.conexion();
        public string _MensajeError = "";
        public string _MensajeSatisfactorio = "";
        public string _PrecioArticulo = "";
        protected void Page_Load(object sender, EventArgs e)
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

                dtDetalleCompra.Columns.Add("ID_PROVEEDOR");
                dtDetalleCompra.Columns.Add("ID_ARTICULO");
                dtDetalleCompra.Columns.Add("NOMBRE_ARTICULO");
                dtDetalleCompra.Columns.Add("PRECIO_ARTICULO");
                dtDetalleCompra.Columns.Add("CANTIDAD_ARTICULO");
                if (!IsPostBack)
                {
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
        private void CargarDatos()
        {
            try
            {
                _Conexion.conectar();
                DataTable dtDatos = new DataTable();
                dtDatos = _Conexion.CargarDatos("select id_proveedor, nombre_proveedor from proveedor order by id_proveedor");
                if (dtDatos.Rows.Count > 0)
                {
                    ListItem _PrimeraOpcion = new ListItem("Seleccione proveedor", "0");
                    cmbProveedores.DataSource = dtDatos;
                    cmbProveedores.DataValueField = "id_proveedor";
                    cmbProveedores.DataTextField = "nombre_proveedor";
                    cmbProveedores.DataBind();
                    cmbProveedores.Items.Insert(0, _PrimeraOpcion);
                }
                txtFecha.Text = DateTime.Now.ToShortDateString();

                // Obtener datos de compra
                string operacion = Request.QueryString["op"];
                if (operacion == "4") //consultar
                {
                    string idCompra = Request.QueryString["idc"];
                    _Conexion.conectar();
                    dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select id_proveedor, fecha_compra from compra where id_compra = " + idCompra);
                    foreach (DataRow _Compra in dtDatos.Rows)
                    {
                        cmbProveedores.SelectedValue = _Compra["id_proveedor"].ToString();
                        txtFecha.Text = DateTime.Parse(_Compra["fecha_compra"].ToString()).ToShortDateString();
                    }
                    _Conexion.conectar();
                    dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select com.id_proveedor, cod.id_articulo, cod.id_articulo, art.nombre_articulo, cod.cantidad_articulo, art.precio_articulo from compra com inner join compra_detalle cod on com.id_compra = cod.id_compra inner join articulos art on cod.id_articulo = art.id_articulo where com.id_compra= " + idCompra);
                    dgvDetalleCompra.DataSource = dtDatos;
                    dgvDetalleCompra.DataBind();
                    btnGuardar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow _GridViewRow in dgvDetalleCompra.Rows)
                {
                    dtDetalleCompra.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text));
                }
                _PrecioArticulo = "";
                if (ObtenerPrecioArticulo())
                {
                    dtDetalleCompra.Rows.Add(cmbProveedores.SelectedValue, cmbArticulos.SelectedValue, cmbArticulos.SelectedItem.Text, _PrecioArticulo, txtCantidad.Text);
                }
                dgvDetalleCompra.DataSource = dtDetalleCompra;
                dgvDetalleCompra.DataBind();
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }

        protected void dgvDetalleCompra_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                foreach (GridViewRow _GridViewRow in dgvDetalleCompra.Rows)
                {
                    dtDetalleCompra.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text));
                }
                dtDetalleCompra.Rows.RemoveAt(e.RowIndex);
                dgvDetalleCompra.DataSource = dtDetalleCompra;
                dgvDetalleCompra.DataBind();
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
                foreach (GridViewRow _GridViewRow in dgvDetalleCompra.Rows)
                {
                    dtDetalleCompra.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text));
                }
                try
                {
                    Negocio.Compra _Compra = new Negocio.Compra();
                    _Compra.IdProveedor = cmbProveedores.SelectedValue.ToString();
                    _Compra.dtDetalleCompra = dtDetalleCompra;
                    if (_Compra.OperarCompra())
                    {
                        float montoTotal = 0;
                        foreach (DataRow _Fila in dtDetalleCompra.Rows)
                        {
                            montoTotal += float.Parse(_Fila["precio_articulo"].ToString()) * float.Parse(_Fila["cantidad_articulo"].ToString());
                        }
                        _MensajeSatisfactorio = "Se ha realizado la compra con un monto total de " + montoTotal;
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        _MensajeError = "Error al grabar compra " + _Compra.Mensaje;
                    }
                }
                catch (Exception ex)
                {
                    _MensajeError = ex.Message;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
        public bool ObtenerPrecioArticulo()
        {
            bool _resultado = true;
            try
            {
                FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                if (_Conexion.conectar())
                {
                    DataTable dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select precio_articulo from articulos where id_articulo = " + cmbArticulos.SelectedValue.ToString());
                    if (dtDatos.Rows.Count > 0)
                    {
                        _PrecioArticulo = dtDatos.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
                return false;
            }
            return _resultado;
        }

        protected void cmbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _Conexion.conectar();
                DataTable dtDatos = new DataTable();
                dtDatos = _Conexion.CargarDatos("select art.id_articulo, art.nombre_articulo from articulos art inner join proveedor pro on art.id_proveedor = pro.id_proveedor where pro.id_proveedor= " + cmbProveedores.SelectedValue + " order by art.id_articulo");
                if (dtDatos.Rows.Count > 0)
                {
                    ListItem _PrimeraOpcion = new ListItem("Seleccione articulo", "0");
                    cmbArticulos.DataSource = dtDatos;
                    cmbArticulos.DataValueField = "id_articulo";
                    cmbArticulos.DataTextField = "nombre_articulo";
                    cmbArticulos.DataBind();
                    cmbArticulos.Items.Insert(0, _PrimeraOpcion);
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
    }
}