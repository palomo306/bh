using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.Web.Clases;
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
using BHermanos.Zonificacion.WebService.Models;


namespace BHermanos.Zonificacion.Web.Modules.Admin
{
    public partial class UsersChild : System.Web.UI.Page
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

        private List<BE.Usuario> ListUsuarios
        {
            get
            {
                if (ViewState["DatosUsuarios"] != null)
                {
                    ObjectStateFormatter _formatter = new ObjectStateFormatter();
                    string vsString = ViewState["DatosUsuarios"].ToString();
                    byte[] bytes = Convert.FromBase64String(vsString);
                    bytes = Compressor.Decompress(bytes);
                    return (List<BE.Usuario>)_formatter.Deserialize(Convert.ToBase64String(bytes));
                }
                else
                    return null;
            }
            set
            {
                ObjectStateFormatter _formatter = new ObjectStateFormatter();
                MemoryStream ms = new MemoryStream();
                _formatter.Serialize(ms, value);
                byte[] ValueArray = ms.ToArray();
                string ValueArrayCompressed = Convert.ToBase64String(Compressor.Compress(ValueArray));
                if (ViewState["DatosUsuarios"] != null)
                    ViewState["DatosUsuarios"] = ValueArrayCompressed;
                else
                    ViewState.Add("DatosUsuarios", ValueArrayCompressed);
            }
        }
        #endregion

        #region Carga de Datos
        private void LoadUsuarios()
        {
            string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
            string appId = ConfigurationManager.AppSettings["AppId"].ToString();
            url += "Usuario/GetUsuario?type=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 20000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
            if (objResponse.Succes)
            {
                this.ListUsuarios = objResponse.ListaUsuarios.ToList();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
            }
        }

        private void LoadRoles()
        {
            List<BE.Rol> lstRoles;
            string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
            url += "Rol/GetRol?type=json";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 20000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            RolModel objResponse = JsonSerializer.Parse<RolModel>(streamReader.ReadToEnd());
            if (objResponse.Succes)
            {
                lstRoles = objResponse.ListaRoles.ToList();
                cblRoles.DataSource = lstRoles;
                cblRoles.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
            }
        }

        private void PrintUsers()
        {
            tableNoData.Visible = false;
            dgUsers.Visible = false;
            if (ListUsuarios != null && ListUsuarios.Count > 0)
            {
                dgUsers.Visible = true;
                dgUsers.DataSource = null;
                dgUsers.DataBind();
                dgUsers.DataSource = ListUsuarios;
                dgUsers.DataBind();
            }
            else
            {
                tableNoData.Visible = true;
            }
        }
        #endregion

        #region Carga de la Página
        private void ValidaUser()
        {
            if (this.CurrentUser == null)
                Response.Redirect("~/Login/Login.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "InitializeScreen();", true);
            ValidaUser();
            if (!Page.IsPostBack)
            {
                LoadUsuarios();
                PrintUsers();
                LoadRoles();
            }
        }
        #endregion

        #region Eventos del Grid
        protected void dgUsers_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgUsers.CurrentPageIndex = e.NewPageIndex;
            PrintUsers();
        }

        protected void dgUsers_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                BE.Usuario currUser = ListUsuarios.Where(usr => usr.Usr == e.Item.Cells[0].Text.Trim()).FirstOrDefault();
                //Se llenan los datos
                spanTitle.InnerText = "Editar Usuario";
                txtId.Text = e.Item.Cells[0].Text;
                txtId.Enabled = false;
                txtNombre.Text = e.Item.Cells[1].Text;
                txtCorreo.Text = e.Item.Cells[2].Text;
                foreach (ListItem item in cblRoles.Items)
                {
                    if (currUser.UserRoles.Where(ur => ur.Id.ToString() == item.Value).Any())
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenNewWindow();", true);
            }
            else if (e.CommandName == "ChangeStatus")
            {
                hdnDelete.Value = e.Item.Cells[0].Text;
                hdnDeleteType.Value = e.CommandArgument.ToString();
                spanDelete.InnerText = "Desactivación de Usuario";
                if (hdnDeleteType.Value != "0")
                    lblDelete.Text = "¿Está seguro que desea desactivar al usuario [" + e.Item.Cells[1].Text + "]?";
                else
                    lblDelete.Text = "¿Está seguro que desea reactivar al usuario [" + e.Item.Cells[1].Text + "]?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenDeleteWindow();", true);
            }
            else if (e.CommandName == "ResetPassword")
            {
                hdnPassword.Value = e.Item.Cells[0].Text;
                spanPassword.InnerText = "Reseteo de Contraseña";
                lblPassword.Text = "¿Está seguro que desea resetear la contraseña del usuario [" + e.Item.Cells[1].Text + "]?";
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenPasswordWindow();", true);
            }
        }
        #endregion

        #region Nuevo Usuario
        protected void btnNew_Click(object sender, EventArgs e)
        {
            spanTitle.InnerText = "Nuevo Usuario";
            foreach (ListItem item in cblRoles.Items)
            {
                item.Selected = false;
            }
            txtId.Text = string.Empty;
            txtId.Enabled = true;
            txtNombre.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "OpenNewWindow();", true);
        }

        private string GetRolesJsonString()
        {
            string result = string.Empty;
            foreach (ListItem item in cblRoles.Items)
            {
                if (item.Selected)
                    result += @"{""<ListMenus>k__BackingField"":[],""<Id>k__BackingField"":" + item.Value + @",""<Nombre>k__BackingField"":""" + item.Text + @"""},";
            }
            if (!string.IsNullOrEmpty(result))
                return "[" + result.Remove(result.Length - 1, 1) + "]";
            return "null";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!txtId.Enabled)
                {
                    //Se reliza la operacion
                    string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                    url += "Usuario/PutUsuario/3?type=json";
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.ContentType = "application/json; charset=utf-8";
                    request.Method = "PUT";
                    request.Timeout = 20000;
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        string json = @"{""<Usr>k__BackingField"":""" + txtId.Text + @""",""<Nombre>k__BackingField"":""" + txtNombre.Text + @""",""<Mail>k__BackingField"":""" + txtCorreo.Text + @""",""<Password>k__BackingField"":"""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":" + GetRolesJsonString() + "}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                    //Se recarga la informacion de usuarios
                    if (objResponse.Succes)
                    {
                        this.ListUsuarios = objResponse.ListaUsuarios.ToList();
                        PrintUsers();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
                    }
                }
                else
                {
                    //Se reliza la operacion
                    string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                    url += "Usuario/PostUsuario?type=json";
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.ContentType = "application/json; charset=utf-8";
                    request.Method = "POST";
                    request.Timeout = 20000;
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        string json = @"{""<Usr>k__BackingField"":""" + txtId.Text + @""",""<Nombre>k__BackingField"":""" + txtNombre.Text + @""",""<Mail>k__BackingField"":""" + txtCorreo.Text + @""",""<Password>k__BackingField"":"""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":" + GetRolesJsonString() + "}";
                        streamWriter.Write(json);
                        streamWriter.Flush();
                    }
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                    //Se recarga la informacion de usuarios
                    if (objResponse.Succes)
                    {
                        this.ListUsuarios = objResponse.ListaUsuarios.ToList();
                        PrintUsers();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Eliminación (desactivación) de usuario
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //Se deshabilita al usuario
                //Se reliza la operacion
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                if (hdnDeleteType.Value != "0")
                    url += "Usuario/PutUsuario/2?type=json";
                else
                    url += "Usuario/PutUsuario/5?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = @"{""<Usr>k__BackingField"":""" + hdnDelete.Value + @""",""<Nombre>k__BackingField"":""" + txtNombre.Text + @""",""<Mail>k__BackingField"":""" + txtCorreo.Text + @""",""<Password>k__BackingField"":"""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":null}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios                
                if (objResponse.Succes)
                {
                    this.ListUsuarios = objResponse.ListaUsuarios.ToList();
                    PrintUsers();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region Reseteo de contraseña del usuario
        protected void btnPassword_Click(object sender, EventArgs e)
        {
            try
            {
                //Se deshabilita al usuario
                //Se reliza la operacion
                string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                url += "Usuario/PutUsuario/1?type=json";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.ContentType = "application/json; charset=utf-8";
                request.Method = "PUT";
                request.Timeout = 20000;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = @"{""<Usr>k__BackingField"":""" + hdnPassword.Value + @""",""<Nombre>k__BackingField"":""" + txtNombre.Text + @""",""<Mail>k__BackingField"":""" + txtCorreo.Text + @""",""<Password>k__BackingField"":"""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":null}";
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                //Se recarga la informacion de usuarios
                if (objResponse.Succes)
                {
                    this.ListUsuarios = objResponse.ListaUsuarios.ToList();
                    PrintUsers();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Reseteo de contraseña','Se ha restablecido correctamente el password del usuario [" + hdnPassword.Value + "].');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "ShowMessage('Error de datos','Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].');", true);
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}