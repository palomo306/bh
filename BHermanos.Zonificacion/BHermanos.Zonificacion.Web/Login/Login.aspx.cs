using BE = BHermanos.Zonificacion.BusinessEntities;
using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BHermanos.Zonificacion.Web.Login
{
    public partial class Login : System.Web.UI.Page
    {
        #region Propiedades
        public BE.Usuario CurrentUser
        {
            get
            {
                if (Session["User"] != null)
                    return (BE.Usuario)Session["User"];
                return null;
            }
            set
            {
                Session["User"] = value;
            }
        }
        #endregion

        #region Carga de la página
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "PrepareDocument();", true);
        }
        #endregion

        #region Boton de Login
        protected void signin_submit_Click(object sender, EventArgs e)
        {
            try
            {
                username.Value = username.Value.Trim();
                password.Value = password.Value.Trim();
                if (!string.IsNullOrEmpty(username.Value.Trim()) && !string.IsNullOrEmpty(password.Value.Trim()))
                {
                    string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                    string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                    url += "Acceso/GetAcceso/" + username.Value + "/" + password.Value + "/" + appId + "?type=json";
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Timeout = 20000;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    AccesoModel objResponse = JsonSerializer.Parse<AccesoModel>(streamReader.ReadToEnd());
                    if (objResponse.Accesa)
                    {
                        this.CurrentUser = objResponse.DatosUsuario;
                        if (this.CurrentUser.Estatus.ToString() == "1")
                            Response.Redirect("~/MainForm.aspx");
                        else if (this.CurrentUser.Estatus.ToString() == "2")
                            Response.Redirect("~/Login/ResetPassword.aspx");
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','El usuario con el que está tratando de acceder, está desactivado.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','Ha ocurrido un error al validar los datos del usuario [" + objResponse.Mensaje + "]');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','Por favor escriba su usuario y contraseña.');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','Ha ocurrido un error al validar los datos del usuario [" + ex.Message + "]');", true);
            }
        }
        #endregion
    }
}