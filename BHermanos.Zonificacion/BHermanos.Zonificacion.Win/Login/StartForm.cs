using BHermanos.Zonificacion.Win.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHermanos.Zonificacion.Win.Login
{
    public partial class StartForm : Form
    {
        #region Campos
        private bool FirstTime;
        #endregion

        #region Constructor
        public StartForm()
        {
            InitializeComponent();
            FirstTime = true;
        }
        #endregion

        #region Botones de Acción
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login oNewLogin = new Login();
            oNewLogin.StartPosition = FormStartPosition.CenterScreen;
            oNewLogin.FormClosed += oNewLogin_FormClosed;
            oNewLogin.ShowDialog();
        }

        void oNewLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Se cierra el formulario de login
            Login oLogin = (Login)sender;
            
            if (oLogin.Loged)
            {
                oLogin.Visible = false;
                this.Visible = false;
                if (Context.CurrentUser.Estatus == 1)
                {
                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();   
                }
                else if (Context.CurrentUser.Estatus == 2)
                {
                    ResetPassword resetForm = new ResetPassword();
                    resetForm.FormClosed += resetForm_FormClosed;
                    resetForm.ShowDialog();   
                }
                else
                {
                    MessageBox.Show("El usuario con el que está tratando de acceder, está desactivado.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

        void resetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResetPassword resetForm = (ResetPassword)sender;
            if (resetForm.DialogResult == DialogResult.OK)
            {
                resetForm.Visible = false;
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();   
            }
            else
            {
                this.Visible = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Carga del Formulario
        private void StartForm_Load(object sender, EventArgs e)
        {
            
        }        

        private void StartForm_Shown(object sender, EventArgs e)
        {
            if (FirstTime)
            {
                btnLogin_Click(null, null);
                FirstTime = false;
            }
        }
        #endregion
    }
}
