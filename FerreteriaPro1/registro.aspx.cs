using FerreteriaPro1.Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class registro : System.Web.UI.Page
    {
        public string _MensajeError = "";
        public string _MensajeSatisfactorio = "";
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                if (_Conexion.conectar())
                {
                    _MensajeError = "Conexión exitosa";
                    DataTable dtRoles = new DataTable();
                    dtRoles = _Conexion.CargarDatos("select id_rol, rol from rol order by id_rol");
                    if (dtRoles.Rows.Count > 0)
                    {
                        ListItem _PrimeraOpcionRol = new ListItem("Seleccione rol", "0");
                        cmbRole.DataSource = dtRoles;
                        cmbRole.DataValueField = "id_rol";
                        cmbRole.DataTextField = "rol";
                        cmbRole.DataBind();
                        cmbRole.Items.Insert(0, _PrimeraOpcionRol);
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
                Usuario usuario = new Usuario();
                usuario.NombreUsuario = txtUsuario.Text.Trim();
                usuario.Contraseña = txtContrasena.Text.Trim();
                usuario.Nombres = txtNombres.Text.Trim();
                usuario.Telefono = txtTelefono.Text.Trim();
                usuario.IdRol = cmbRole.SelectedValue;

                if (usuario.OperarUsuario())
                {
                    _MensajeSatisfactorio = "Usuario agregado correctamente";
                    btnGuardar.Enabled = false;
                }
                else
                {
                    _MensajeError = "Error al grabar al usuario: " + usuario.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _MensajeError = ex.Message;
            }
        }
    }
}