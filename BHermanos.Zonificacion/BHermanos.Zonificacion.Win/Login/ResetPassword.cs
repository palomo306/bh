using BHermanos.Zonificacion.BusinessEntities.Cast;
using BHermanos.Zonificacion.WebService.Models;
using BHermanos.Zonificacion.Win.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHermanos.Zonificacion.Win.Login
{
    public partial class ResetPassword : Form
    {
        #region Constructor
        public ResetPassword()
        {
            InitializeComponent();
            lblMessage.Text = "Estimado " + Context.CurrentUser.Nombre + ", por favor escriba su contraseña y confírmelo para restablecerla.";
        }
        #endregion

        #region Botones
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Text = txtPassword.Text.Trim();
                txtPassword1.Text = txtPassword1.Text.Trim();
                if (!string.IsNullOrEmpty(txtPassword.Text.Trim()) && !string.IsNullOrEmpty(txtPassword1.Text.Trim()))
                {
                    if (txtPassword.Text == txtPassword1.Text)
                    {
                        //Se reliza la operacion
                        string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                        url += "Usuario/PutUsuario/4?type=json";
                        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                        request.ContentType = "application/json; charset=utf-8";
                        request.Method = "PUT";
                        request.Timeout = 20000;
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            string json = @"{""<Usr>k__BackingField"":""" + Context.CurrentUser.Usr + @""",""<Nombre>k__BackingField"":""" + Context.CurrentUser.Nombre + @""",""<Mail>k__BackingField"":""" + Context.CurrentUser.Mail + @""",""<Password>k__BackingField"":""" + txtPassword.Text + @""",""<Estatus>k__BackingField"":1,""<UserRoles>k__BackingField"":null}";
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        UsuarioModel objResponse = JsonSerializer.Parse<UsuarioModel>(streamReader.ReadToEnd());
                        //Se recarga la informacion de usuarios
                        if (objResponse.Succes)
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Ha ocurrido un error al carga los datos de usuarios desde el servidor [" + objResponse.Mensaje + "].");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor escriba su usuario y contraseña.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al validar los datos del usuario [" + ex.Message + "].");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}