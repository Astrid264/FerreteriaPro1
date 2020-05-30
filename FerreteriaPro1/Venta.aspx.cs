using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Venta : System.Web.UI.Page
    {
        DataTable dtDetalleVenta = new DataTable();
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

                dtDetalleVenta.Columns.Add("ID_FERRETERIA");
                dtDetalleVenta.Columns.Add("ID_CLIENTE");
                dtDetalleVenta.Columns.Add("ID_ARTICULO");
                dtDetalleVenta.Columns.Add("NOMBRE_ARTICULO");
                dtDetalleVenta.Columns.Add("PRECIO_ARTICULO");
                dtDetalleVenta.Columns.Add("CANTIDAD_ARTICULO");
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
                dtDatos = _Conexion.CargarDatos("select id_ferreteria, nombre from ferreteria order by id_ferreteria");
                if (dtDatos.Rows.Count > 0)
                {
                    ListItem _PrimeraOpcion = new ListItem("Seleccione ferreteria", "0");
                    cmbFerreteria.DataSource = dtDatos;
                    cmbFerreteria.DataValueField = "id_ferreteria";
                    cmbFerreteria.DataTextField = "nombre";
                    cmbFerreteria.DataBind();
                    cmbFerreteria.Items.Insert(0, _PrimeraOpcion);
                }
                _Conexion.conectar();
                dtDatos = new DataTable();
                dtDatos = _Conexion.CargarDatos("select id_cliente, nombre_cliente from clientes order by id_cliente");
                if (dtDatos.Rows.Count > 0)
                {
                    ListItem _PrimeraOpcion = new ListItem("Seleccione cliente", "0");
                    cmbClientes.DataSource = dtDatos;
                    cmbClientes.DataValueField = "id_cliente";
                    cmbClientes.DataTextField = "nombre_cliente";
                    cmbClientes.DataBind();
                    cmbClientes.Items.Insert(0, _PrimeraOpcion);
                }
                txtFecha.Text = DateTime.Now.ToShortDateString();

                // Obtener datos de venta
                string operacion = Request.QueryString["op"];
                if (operacion == "4") //consultar
                {
                    string idVenta = Request.QueryString["idv"];
                    _Conexion.conectar();
                    dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select id_cliente, id_ferreteria, fecha_venta from venta where id_venta = " + idVenta);
                    foreach (DataRow _Venta in dtDatos.Rows)
                    {
                        cmbClientes.SelectedValue = _Venta["id_cliente"].ToString();
                        cmbFerreteria.SelectedValue = _Venta["id_ferreteria"].ToString();
                        txtFecha.Text = DateTime.Parse(_Venta["fecha_venta"].ToString()).ToShortDateString();
                    }
                    _Conexion.conectar();
                    dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select ven.id_ferreteria, ven.id_cliente, ved.id_articulo, art.nombre_articulo, art.precio_articulo, ved.cantidad_articulo from venta ven inner join venta_detalle ved on ven.id_venta = ved.id_venta inner join ARTICULOS art on ved.id_articulo=art.id_articulo where ven.id_venta= " + idVenta);
                    dgvDetalleVenta.DataSource = dtDatos;
                    dgvDetalleVenta.DataBind();
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
                foreach (GridViewRow _GridViewRow in dgvDetalleVenta.Rows)
                {
                    dtDetalleVenta.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[6].Text));
                }
                _PrecioArticulo = "";
                if (ValidarDisponibilidadArticulo())
                {
                    dtDetalleVenta.Rows.Add(cmbFerreteria.SelectedValue, cmbClientes.SelectedValue, cmbArticulos.SelectedValue, cmbArticulos.SelectedItem.Text, _PrecioArticulo, txtCantidad.Text);
                }
                dgvDetalleVenta.DataSource = dtDetalleVenta;
                dgvDetalleVenta.DataBind();
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }

        protected void dgvDetalleVenta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                foreach (GridViewRow _GridViewRow in dgvDetalleVenta.Rows)
                {
                    dtDetalleVenta.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[6].Text));
                }
                dtDetalleVenta.Rows.RemoveAt(e.RowIndex);
                dgvDetalleVenta.DataSource = dtDetalleVenta;
                dgvDetalleVenta.DataBind();
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
                foreach (GridViewRow _GridViewRow in dgvDetalleVenta.Rows)
                {
                    dtDetalleVenta.Rows.Add(_GridViewRow.Cells[1].Text, HttpUtility.HtmlDecode(_GridViewRow.Cells[2].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[3].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[4].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[5].Text), HttpUtility.HtmlDecode(_GridViewRow.Cells[6].Text));
                }
                try
                {
                    Negocio.Venta _Venta = new Negocio.Venta();
                    _Venta.IdFerreteria = cmbFerreteria.SelectedValue.ToString();
                    _Venta.IdCliente = cmbClientes.SelectedValue.ToString();
                    _Venta.dtDetalleVenta = dtDetalleVenta;
                    if (_Venta.OperarVenta())
                    {
                        float montoTotal = 0;
                        foreach (DataRow _Fila in dtDetalleVenta.Rows)
                        {
                            montoTotal += float.Parse(_Fila["precio_articulo"].ToString()) * float.Parse(_Fila["cantidad_articulo"].ToString());
                        }
                        _MensajeSatisfactorio = "Se ha realizado la venta con un monto total de " + montoTotal;
                        btnGuardar.Enabled = false;
                    }
                    else
                    {
                        _MensajeError = "Error al grabar " + _Venta.Mensaje;
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
        public bool ValidarDisponibilidadArticulo()
        {
            bool _resultado = true;
            try
            {
                FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
                if (_Conexion.conectar())
                {
                    DataTable dtDatos = new DataTable();
                    dtDatos = _Conexion.CargarDatos("select cantidad_articulo_ferreteria, precio_articulo from articulos art inner join articulo_ferreteria arf on art.id_articulo = arf.id_articulo where arf.id_ferreteria = " + cmbFerreteria.SelectedValue + " and art.id_articulo = " + cmbArticulos.SelectedValue.ToString());
                    if (dtDatos.Rows.Count > 0)
                    {
                        int stockArticulo = int.Parse(dtDatos.Rows[0][0].ToString());
                        int cantidadProductosAgregados = 0;
                        cantidadProductosAgregados = ObtenerCantidadArticuloEnGrid();
                        if ((int.Parse(txtCantidad.Text) + cantidadProductosAgregados) > stockArticulo)
                        {
                            _resultado = false;
                            _MensajeError = "La cantidad ingresada es mayor al stock del producto";
                        }
                        else
                        {
                            _PrecioArticulo = dtDatos.Rows[0][1].ToString();
                        }
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
        public int ObtenerCantidadArticuloEnGrid()
        {
            int _resultado = 0;
            try
            {
                foreach (DataRow _Fila in dtDetalleVenta.Rows)
                {
                    if (_Fila["id_articulo"].ToString() == cmbArticulos.SelectedValue.ToString())
                    {
                        _resultado += int.Parse(_Fila["cantidad_articulo"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return _resultado;
        }
        protected void cmbFerreteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _Conexion.conectar();
                DataTable dtDatos = new DataTable();
                dtDatos = _Conexion.CargarDatos("select art.id_articulo, art.nombre_articulo from articulos art inner join articulo_ferreteria arf on art.id_articulo = arf.id_articulo where arf.id_ferreteria = " + cmbFerreteria.SelectedValue + " order by art.id_articulo");
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