using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class login : System.Web.UI.Page
    {
        public FerreteriaPro1.conexion.conexion _Conexion = new FerreteriaPro1.conexion.conexion();
        public string _MensajeSatisfactorio = "";
        public string _MensajeError = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_Conexion.conectar())
                {
                    DataTable dtUsuario = new DataTable();
                    dtUsuario = _Conexion.CargarDatos("select usuario from usuario where usuario = '"+txtUsuario.Text.Trim()+ "' and contraseña = '" + txtContrasena.Text.Trim()+ "'");
                    if (dtUsuario.Rows.Count > 0)
                    {
                        _MensajeSatisfactorio = "Usuario correcto";
                        HttpCookie _IdUsuario = new HttpCookie("idusuario");
                        _IdUsuario.Value = txtUsuario.Text;
                        _IdUsuario.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(_IdUsuario);
                        Response.Redirect("Default.aspx");
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