using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class Ferreteria : System.Web.UI.Page
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

            ObtenerDatosFerreteria();
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }
        private void ObtenerDatosFerreteria()
        {
            try
            {
                if (!IsPostBack)
                {
                    string idFerreteria = Request.QueryString["idf"];
                    string operacion = Request.QueryString["op"];
                    if (operacion == null || operacion == "" || idFerreteria == null || idFerreteria == "")
                    {
                        _MensajeError = "Debe ingresar operacion y codigo de ferreteria";
                    }
                    else
                    {
                        if (operacion == "4")
                        {
                            if (_Conexion.conectar())
                            {
                                DataTable dtFerreteria = new DataTable();
                                dtFerreteria = _Conexion.CargarDatos("select * from ferreteria where id_ferreteria = " + idFerreteria);
                                foreach (DataRow _Ferreteria in dtFerreteria.Rows)
                                {
                                    txtNombres.Text = _Ferreteria["nombre"].ToString();
                                    txtTelefono.Text = _Ferreteria["telefono"].ToString();
                                  cmbTipoFerreteria.SelectedValue= _Ferreteria["id_tipoferreteria"].ToString();
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
                    DataTable dtTipos = new DataTable();
                    dtTipos = _Conexion.CargarDatos("select id_tipoferreteria, tipo_ferreteria from tipo_ferreteria order by id_tipoferreteria");
                    if (dtTipos.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcionTipos = new ListItem("Seleccione Tipo", "0");
                        cmbTipoFerreteria.DataSource = dtTipos;
                        cmbTipoFerreteria.DataValueField = "id_tipoferreteria";
                        cmbTipoFerreteria.DataTextField = "tipo_ferreteria";
                        cmbTipoFerreteria.DataBind();
                        cmbTipoFerreteria.Items.Insert(0, _PrimeraOpcionTipos);
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
                Negocio.Ferreteria _Ferreteria = new Negocio.Ferreteria();
                _Ferreteria.Nombres = txtNombres.Text.Trim();
                _Ferreteria.Telefono = txtTelefono.Text.Trim();
                _Ferreteria.IdTipoFerreteria = cmbTipoFerreteria.SelectedValue.ToString();
                if (_Ferreteria.OperarFerreteria()) 
                {

                    _MensajeSatisfactorio = "Ferreteria agregada correctamente";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar el ferreteria: " + _Ferreteria.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }

        }
    }

}