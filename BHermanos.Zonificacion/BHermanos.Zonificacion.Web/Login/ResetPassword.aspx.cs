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
using BE = BHermanos.Zonificacion.BusinessEntities;


namespace BHermanos.Zonificacion.Web.Login
{
    public partial class ResetPassword : System.Web.UI.Page
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
        private void ValidaUser()
        {
            if (this.CurrentUser == null)
                Response.Redirect("~/Login/Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "PrepareDocument();", true);
            ValidaUser();
            if (!Page.IsPostBack)
            {
                lblNombre.Text = this.CurrentUser.Nombre;
            }
        }
        #endregion

        #region Boton de Login
        protected void signin_submit_Click(object sender, EventArgs e)
        {
            try
            {
                password.Value = password.Value.Trim();
                password1.Value = password.Value.Trim();
                if (!string.IsNullOrEmpty(password.Value.Trim()) && !string.IsNullOrEmpty(password1.Value.Trim()))
                {
                    if (password.Value == password1.Value)
                    {
                        //Se reliza la operacion
                        string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                        int timeOut = int.Parse(ConfigurationManager.AppSettings["ConnectTimeOut"].ToString());
                        url += "Usuario/PutUsuario/4?type=json";
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                        request.ContentType = "application/json; charset=utf-8";
                        request.Method = "PUT";
                        request.Timeout = timeOut;
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            string json = @"{""<Usr>k__BackingField"":""" + CurrentUser.Usr + @""",""<Nombre>k__BackingField"":""" + CurrentUser.Nombre + @""",""<Mail>k__BackingField"":""" + CurrentUser.Mail + @""",""<Password>k__BackingField"":""" + password.Value + @""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":null}";
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                        //Se recarga la informacion de usuarios
                        if (objResponse.Succes)
                        {
                            Response.Redirect("~/MainForm.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','Por favor escriba su usuario y contraseña.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de validación','Ha ocurrido un error al validar los datos del usuario [" + ex.Message + "]');", true);
            }
        }
        #endregion
    }
}