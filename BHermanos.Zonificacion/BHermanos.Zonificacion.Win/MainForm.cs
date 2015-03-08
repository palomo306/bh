using System;
using BE = BHermanos.Zonificacion.BusinessEntities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BHermanos.Zonificacion.Win.Clases;

namespace BHermanos.Zonificacion.Win
{
    public partial class MainForm : Form
    {
        #region Constructor
        private void ProcessSubMenu(BE.Menu menu, ToolStripMenuItem visualMenu)
        {
            if (menu.ListMenus.Count > 0)
            {
                foreach (BE.Menu subMenu in menu.ListMenus)
                {
                    ToolStripMenuItem visulSubMenu = new ToolStripMenuItem(subMenu.Nombre);
                    ProcessSubMenu(subMenu, visulSubMenu);
                    visualMenu.DropDownItems.Add(visulSubMenu);
                }
            }
            else
            {
                visualMenu.Tag = menu;
                visualMenu.Click += visualMenu_Click;
            }
        }

        void visualMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem visulSubMenu = (ToolStripMenuItem)sender;
            BE.Menu menu = (BE.Menu)visulSubMenu.Tag;
            Form form = (Form)Activator.CreateInstance(Type.GetType(menu.Aplicacion));
            form.MdiParent = this;
            form.Show();
        }

        private void CreateMenus()
        {
            List<BE.Menu> lstAllMenus = new List<BE.Menu>();
            foreach (BE.Rol rol in Context.CurrentUser.UserRoles)
            {
                lstAllMenus = lstAllMenus.Union(rol.ListMenus, new BE.Comparer.MenuComparer()).ToList();
            }
            foreach (BE.Menu menu in lstAllMenus.OrderByDescending(mnu => mnu.Orden))
            {
                ToolStripMenuItem newItem = new ToolStripMenuItem(menu.Nombre);
                ProcessSubMenu(menu, newItem);
                menuStrip.Items.Insert(1, newItem);
            }
        }

        public MainForm()
        {
            InitializeComponent();
            CreateMenus();
        }
        #endregion

        #region Menú Archivo
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }
        #endregion

        #region Menú Ventana
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region Cerrado de Formulario Principal
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
        }
        #endregion

    }
}