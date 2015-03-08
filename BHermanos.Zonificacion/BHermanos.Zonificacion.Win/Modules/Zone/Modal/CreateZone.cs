using BE = BHermanos.Zonificacion.BusinessEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHermanos.Zonificacion.Win.Modules.Zone.Modal
{
    public partial class CreateZone : Form
    {
        #region Propiedades
        public string Color
        {
            get
            {
                string r = pnlColor.BackColor.R.ToString("X");
                string g = pnlColor.BackColor.G.ToString("X");
                string b = pnlColor.BackColor.B.ToString("X");
                return r.PadLeft(2, '0') + g.PadLeft(2, '0') + b.PadLeft(2, '0');
            }
            set
            {
                string r = value.Substring(0,2);
                string g = value.Substring(2, 2);
                string b = value.Substring(4, 2);
                pnlColor.BackColor = System.Drawing.Color.FromArgb(255, Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));
            }
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
            set
            {
                txtNombre.Text = value;
            }
        }

        public List<BE.Zona> ListZonas { get; set; }

        public int ZoneEditId { get; set; }
        #endregion

        #region Constructor
        public CreateZone()
        {
            InitializeComponent();
        }
        #endregion

        #region Selección de Color
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.AllowFullOpen = false;
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                pnlColor.BackColor = colorDlg.Color;
            }
        }
        #endregion

        #region Regreso de resultados
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = txtNombre.Text.Trim();
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                BE.Zona existZona = ListZonas.Where(z => z.Nombre.ToLower() == txtNombre.Text.ToLower() && z.Id != ZoneEditId).FirstOrDefault();
                if (existZona == null)
                    this.DialogResult = DialogResult.OK;
                else
                {
                    MessageBox.Show("Ya existe otra zona con el mismo nombre, por favor escriba otro.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNombre.Focus();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un colo y escriba un nombre para la nueva zona", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
            }
        }
        #endregion
    }
}
