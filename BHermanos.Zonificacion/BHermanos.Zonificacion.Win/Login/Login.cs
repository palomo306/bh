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
    public partial class Login : Form
    {
        #region Propiedades
        public bool Loged;
        #endregion

        #region Constructor
        public Login()
        {
            InitializeComponent();
        }
        #endregion

        #region Botones
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                txtUser.Text = txtUser.Text.Trim();
                txtPassword.Text = txtPassword.Text.Trim();
                if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    string url = ConfigurationManager.AppSettings["UrlServiceBase"].ToString();
                    string appId = ConfigurationManager.AppSettings["AppId"].ToString();
                    int connectTimeOut = int.Parse(ConfigurationManager.AppSettings["ConnectTimeOut"].ToString());
                    url += "Acceso/GetAcceso/" + txtUser.Text + "/" + txtPassword.Text + "/" + appId + "?type=json";
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Timeout = connectTimeOut;
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader streamReader = new StreamReader(response.GetResponseStream());
                    AccesoModel objResponse = JsonSerializer.Parse<AccesoModel>(streamReader.ReadToEnd());
                    if (objResponse.Accesa)
                    {
                        Context.CurrentUser = objResponse.DatosUsuario;
                        this.Loged = true;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al validar los datos del usuario [" + objResponse.Mensaje + "]", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor escriba su usuario y contraseña.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al validar los datos del usuario [" + ex.Message + "]", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Loged = false;
            this.Close();
        }
        #endregion
    }
}