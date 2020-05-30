using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FerreteriaPro1
{
    public partial class SiteMaster : MasterPage
    {
        public string _Usuario = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies["idusuario"] != null)
                {
                    if (Request.Cookies["idusuario"].Value == null || Request.Cookies["idusuario"].Value == "")
                    {                        
                    }
                    else
                    {
                        _Usuario = Request.Cookies["idusuario"].Value;
                    }
                }                                    
            }
            catch (Exception ex)
            {

            }
        }
    }
}