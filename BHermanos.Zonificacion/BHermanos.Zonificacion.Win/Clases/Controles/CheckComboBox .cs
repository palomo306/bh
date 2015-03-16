using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BHermanos.Zonificacion.Win.Clases.Controles
{
    public partial class CheckComboBox : ComboBox
    {
        #region Eventos
        public event EventHandler CheckStateChanged;
        #endregion

        #region Constructor
        public CheckComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += new DrawItemEventHandler(CheckComboBox_DrawItem);
            this.SelectedIndexChanged += new EventHandler(CheckComboBox_SelectedIndexChanged);
        }
        #endregion

        #region Métodos
        void CheckComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                return;
            }
            if (!(Items[e.Index] is CheckComboBoxItem))
            {
                e.Graphics.DrawString(Items[e.Index].ToString(), this.Font, Brushes.Black, new Point(e.Bounds.X, e.Bounds.Y));
                return;
            }
            CheckComboBoxItem box = (CheckComboBoxItem)Items[e.Index];
            CheckBoxRenderer.RenderMatchingApplicationState = true;
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(e.Bounds.X, e.Bounds.Y), e.Bounds, box.Text, this.Font, (e.State & DrawItemState.Focus) == 0, box.CheckState ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
        }

        void CheckComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckComboBoxItem item = (CheckComboBoxItem)SelectedItem;
            item.CheckState = !item.CheckState;
            if (CheckStateChanged != null)
                CheckStateChanged(item, e);
        }
        #endregion
    }
}