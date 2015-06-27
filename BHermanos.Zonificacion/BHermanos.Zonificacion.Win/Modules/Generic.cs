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
        private readonly string URL;
        private readonly int ConnectTimeOut;
        private readonly string AppId;

        public Generic(ref ToolStripStatusLabel toolStripStatusLabel, string url, int connectTimeOut, string appId)
        {                     
            InitializeComponent();
            this.toolStripStatusLabel = toolStripStatusLabel;
            this.URL = url;
            this.ConnectTimeOut = connectTimeOut;
            this.AppId = appId;
        }
        
    }
}
