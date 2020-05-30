using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Abastecimiento : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void CargarDatos()
        {
            try
            {
                DataTable dtDatos = new DataTable();
                if (_Conexion.conectar())
                {
                    dtDatos = _Conexion.CargarDatos("select id_articulo, nombre_articulo from articulos order by id_articulo");
                    if (dtDatos.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcion = new ListItem("Seleccione artículo", "0");
                        cmbArticulos.DataSource = dtDatos;
                        cmbArticulos.DataValueField = "id_articulo";
                        cmbArticulos.DataTextField = "nombre_articulo";
                        cmbArticulos.DataBind();
                        cmbArticulos.Items.Insert(0, _PrimeraOpcion);
                    }
                    if (_Conexion.conectar())
                    {
                        dtDatos = new DataTable();
                        dtDatos = _Conexion.CargarDatos("select id_ferreteria, nombre from ferreteria order by id_ferreteria");
                        if (dtDatos.Rows.Count > 0)
                        {
                            ListItem _PrimeraOpcion = new ListItem("Seleccione ferretería", "0");
                            cmbIdFerreteria.DataSource = dtDatos;
                            cmbIdFerreteria.DataValueField = "id_ferreteria";
                            cmbIdFerreteria.DataTextField = "nombre";
                            cmbIdFerreteria.DataBind();
                            cmbIdFerreteria.Items.Insert(0, _PrimeraOpcion);
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
                Negocio.Abastecimiento _Abastecimiento = new Negocio.Abastecimiento();
                _Abastecimiento.IdArticulo = cmbArticulos.SelectedValue.ToString();
                _Abastecimiento.Cantidad = txtCantidad.Text.Trim();
                _Abastecimiento.IdFerreteria = cmbIdFerreteria.SelectedValue.ToString();
                if (_Abastecimiento.OperarAbastecimiento())
                {

                    _MensajeSatisfactorio = "Se han trasladado " + txtCantidad.Text + " articulos de " + cmbArticulos.SelectedItem.Text + " a la ferretería " + cmbIdFerreteria.SelectedItem.Text;
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar " + _Abastecimiento.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
    }
}