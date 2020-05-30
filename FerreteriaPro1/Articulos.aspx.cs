using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class ARTICULOS : System.Web.UI.Page
    {
        public string _MensajeError = "";
        public string _MensajeSatisfactorio = "";
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();

        public object cmbTipos { get; private set; }

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

            ObtenerDatosArticulo();
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void ObtenerDatosArticulo()
        {
            try
            {
                if (!IsPostBack)
                {
                    string idArticulos = Request.QueryString["ida"];
                    string operacion = Request.QueryString["op"];
                    if (operacion == null || operacion == "" || idArticulos == null || idArticulos == "")
                    {
                        _MensajeError = "Debe ingresar operacion y codigo de articulos";
                    }
                    else
                    {
                        if (operacion == "4")
                        {
                            if (_Conexion.conectar())
                            {
                                DataTable dtArticulos = new DataTable();
                                dtArticulos = _Conexion.CargarDatos("select * from articulos where id_articulos = " + idArticulos);
                                foreach (DataRow _Ferreteria in dtArticulos.Rows)
                                {
                                    txtNombreArticulo.Text = _Ferreteria["nombre_articulo"].ToString();
                                    txtDescripcionArticulo.Text = _Ferreteria["descripcion_articulo"].ToString();
                                    txtPrecioArticulo.Text = _Ferreteria["precio_articulo"].ToString();
                                    txtStockArticulo.Text = _Ferreteria["stock_articulo"].ToString();
                                    cmbUnidadMedida.SelectedValue = _Ferreteria["id_unidad_medida"].ToString();
                                    cmbProveedor.SelectedValue = _Ferreteria["id_proveedor"].ToString();
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
        private void CargarDatos()
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtProveedores = new DataTable();
                    dtProveedores = _Conexion.CargarDatos("select id_proveedor, nombre_proveedor from proveedor order by id_proveedor");
                    if (dtProveedores.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcionProveedores = new ListItem("Seleccione Proveedor", "0");
                        cmbProveedor.DataSource = dtProveedores;
                        cmbProveedor.DataValueField = "id_proveedor";
                        cmbProveedor.DataTextField = "nombre_proveedor";
                        cmbProveedor.DataBind();
                        cmbProveedor.Items.Insert(0, _PrimeraOpcionProveedores);
                    }
                    if (_Conexion.conectar())
                    {
                        DataTable dtUnidades = new DataTable();
                    dtUnidades = _Conexion.CargarDatos("select id_unidad_medida, unidad_medida from Unidad_medida order by id_unidad_medida");
                    if (dtUnidades.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcionUnidades = new ListItem("Seleccione Unidad_Medida", "0");
                        cmbUnidadMedida.DataSource = dtUnidades;
                        cmbUnidadMedida.DataValueField = "id_unidad_medida";
                        cmbUnidadMedida.DataTextField = "unidad_medida";
                        cmbUnidadMedida.DataBind();
                        cmbUnidadMedida.Items.Insert(0, _PrimeraOpcionUnidades);
                    }
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Negocio.Articulos _Articulos = new Negocio.Articulos();
                _Articulos.NombreArticulo = txtNombreArticulo.Text.Trim();
                _Articulos.DescripcionArticulo = txtDescripcionArticulo.Text.Trim();
                _Articulos.PrecioArticulo = txtPrecioArticulo.Text.Trim();
                _Articulos.StockArticulo= txtStockArticulo.Text.Trim();
                _Articulos.IdProveedor = cmbProveedor.SelectedValue.ToString();
                _Articulos.IdUnidadMedida = cmbUnidadMedida.SelectedValue.ToString();
                if (_Articulos.OperarArticulos())
                {

                    _MensajeSatisfactorio = "Articulo agregado correctamente";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar el articulo " + _Articulos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
    }
}