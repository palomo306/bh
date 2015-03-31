using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHermanos.Zonificacion.Win.Modules
{
    public partial class Generic : Form
    {
        public ToolStripStatusLabel toolStripStatusLabel = null;

        public Generic(ref ToolStripStatusLabel toolStripStatusLabel)
        {
            this.toolStripStatusLabel = toolStripStatusLabel;            
            InitializeComponent();
        }
        
    }
}
