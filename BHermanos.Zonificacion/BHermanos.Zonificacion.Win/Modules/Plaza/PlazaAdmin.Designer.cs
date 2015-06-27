namespace BHermanos.Zonificacion.Win.Modules.Plaza
{
    partial class PlazaAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlazaAdmin));
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlHeadFields = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMunicipio = new System.Windows.Forms.ComboBox();
            this.ccbEstados = new BHermanos.Zonificacion.Win.Clases.Controles.CheckComboBox();
            this.txtCurrentPlaza = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitterTreeView = new NJFLib.Controls.CollapsibleSplitter();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCanelZone = new System.Windows.Forms.Button();
            this.btnSaveZone = new System.Windows.Forms.Button();
            this.dgPlazas = new System.Windows.Forms.DataGridView();
            this.Plaza = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Ver = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VerCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.panelCtrlMap = new System.Windows.Forms.Panel();
            this.pbbZoomMax = new System.Windows.Forms.PictureBox();
            this.pbbZoomSelect = new System.Windows.Forms.PictureBox();
            this.pbbZoomMin = new System.Windows.Forms.PictureBox();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.sfmMainMap = new EGIS.Controls.SFMap();
            this.pnlHead.SuspendLayout();
            this.pnlHeadFields.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPlazas)).BeginInit();
            this.pnlMap.SuspendLayout();
            this.panelCtrlMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.Controls.Add(this.pnlHeadFields);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(812, 50);
            this.pnlHead.TabIndex = 2;
            // 
            // pnlHeadFields
            // 
            this.pnlHeadFields.Controls.Add(this.label3);
            this.pnlHeadFields.Controls.Add(this.cmbMunicipio);
            this.pnlHeadFields.Controls.Add(this.ccbEstados);
            this.pnlHeadFields.Controls.Add(this.txtCurrentPlaza);
            this.pnlHeadFields.Controls.Add(this.label2);
            this.pnlHeadFields.Controls.Add(this.label1);
            this.pnlHeadFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeadFields.Location = new System.Drawing.Point(99, 0);
            this.pnlHeadFields.Name = "pnlHeadFields";
            this.pnlHeadFields.Size = new System.Drawing.Size(711, 48);
            this.pnlHeadFields.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Municipio:";
            // 
            // cmbMunicipio
            // 
            this.cmbMunicipio.FormattingEnabled = true;
            this.cmbMunicipio.Location = new System.Drawing.Point(217, 22);
            this.cmbMunicipio.Name = "cmbMunicipio";
            this.cmbMunicipio.Size = new System.Drawing.Size(330, 21);
            this.cmbMunicipio.TabIndex = 11;
            this.cmbMunicipio.SelectedIndexChanged += new System.EventHandler(this.cmbMunicipio_SelectedIndexChanged);
            // 
            // ccbEstados
            // 
            this.ccbEstados.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ccbEstados.FormattingEnabled = true;
            this.ccbEstados.Location = new System.Drawing.Point(11, 22);
            this.ccbEstados.Name = "ccbEstados";
            this.ccbEstados.Size = new System.Drawing.Size(200, 21);
            this.ccbEstados.TabIndex = 10;
            this.ccbEstados.CheckStateChanged += new System.EventHandler(this.ccbEstados_CheckStateChanged);
            // 
            // txtCurrentPlaza
            // 
            this.txtCurrentPlaza.Location = new System.Drawing.Point(553, 22);
            this.txtCurrentPlaza.Name = "txtCurrentPlaza";
            this.txtCurrentPlaza.ReadOnly = true;
            this.txtCurrentPlaza.Size = new System.Drawing.Size(150, 20);
            this.txtCurrentPlaza.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Plaza:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Estado:";
            // 
            // splitterTreeView
            // 
            this.splitterTreeView.AnimationDelay = 20;
            this.splitterTreeView.AnimationStep = 20;
            this.splitterTreeView.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.splitterTreeView.ControlToHide = this.pnlInfo;
            this.splitterTreeView.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterTreeView.ExpandParentForm = true;
            this.splitterTreeView.Location = new System.Drawing.Point(494, 50);
            this.splitterTreeView.Name = "splitterTreeView";
            this.splitterTreeView.TabIndex = 32;
            this.splitterTreeView.TabStop = false;
            this.splitterTreeView.UseAnimations = false;
            this.splitterTreeView.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.panel2);
            this.pnlInfo.Controls.Add(this.dgPlazas);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlInfo.Location = new System.Drawing.Point(502, 50);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(5);
            this.pnlInfo.Size = new System.Drawing.Size(310, 523);
            this.pnlInfo.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCanelZone);
            this.panel2.Controls.Add(this.btnSaveZone);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 468);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 50);
            this.panel2.TabIndex = 2;
            // 
            // btnCanelZone
            // 
            this.btnCanelZone.Location = new System.Drawing.Point(197, 8);
            this.btnCanelZone.Name = "btnCanelZone";
            this.btnCanelZone.Size = new System.Drawing.Size(100, 29);
            this.btnCanelZone.TabIndex = 5;
            this.btnCanelZone.Text = "Cancelar";
            this.btnCanelZone.UseVisualStyleBackColor = true;
            this.btnCanelZone.Click += new System.EventHandler(this.btnCanelZone_Click);
            // 
            // btnSaveZone
            // 
            this.btnSaveZone.Location = new System.Drawing.Point(91, 8);
            this.btnSaveZone.Name = "btnSaveZone";
            this.btnSaveZone.Size = new System.Drawing.Size(100, 29);
            this.btnSaveZone.TabIndex = 4;
            this.btnSaveZone.Text = "Aceptar";
            this.btnSaveZone.UseVisualStyleBackColor = true;
            this.btnSaveZone.Click += new System.EventHandler(this.btnSaveZone_Click);
            // 
            // dgPlazas
            // 
            this.dgPlazas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPlazas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Plaza,
            this.Editar,
            this.Delete,
            this.Ver,
            this.Id,
            this.VerCol});
            this.dgPlazas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPlazas.Location = new System.Drawing.Point(5, 5);
            this.dgPlazas.Margin = new System.Windows.Forms.Padding(10);
            this.dgPlazas.Name = "dgPlazas";
            this.dgPlazas.RowHeadersVisible = false;
            this.dgPlazas.Size = new System.Drawing.Size(300, 513);
            this.dgPlazas.TabIndex = 1;
            this.dgPlazas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPlazas_CellClick);
            this.dgPlazas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgPlazas_DataBindingComplete);
            // 
            // Plaza
            // 
            this.Plaza.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Plaza.DataPropertyName = "Nombre";
            this.Plaza.HeaderText = "Plaza";
            this.Plaza.Name = "Plaza";
            this.Plaza.ReadOnly = true;
            // 
            // Editar
            // 
            this.Editar.DataPropertyName = "Editar";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.Editar.DefaultCellStyle = dataGridViewCellStyle1;
            this.Editar.HeaderText = "";
            this.Editar.MinimumWidth = 55;
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Text = "Editar";
            this.Editar.Width = 55;
            // 
            // Delete
            // 
            this.Delete.DataPropertyName = "Eliminar";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            this.Delete.DefaultCellStyle = dataGridViewCellStyle2;
            this.Delete.HeaderText = "";
            this.Delete.MinimumWidth = 55;
            this.Delete.Name = "Delete";
            this.Delete.ReadOnly = true;
            this.Delete.Text = "Eliminar";
            this.Delete.Width = 55;
            // 
            // Ver
            // 
            this.Ver.DataPropertyName = "Ver";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(1);
            this.Ver.DefaultCellStyle = dataGridViewCellStyle3;
            this.Ver.HeaderText = "";
            this.Ver.MinimumWidth = 55;
            this.Ver.Name = "Ver";
            this.Ver.Width = 55;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // VerCol
            // 
            this.VerCol.DataPropertyName = "VerCol";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(1);
            this.VerCol.DefaultCellStyle = dataGridViewCellStyle4;
            this.VerCol.HeaderText = "";
            this.VerCol.MinimumWidth = 55;
            this.VerCol.Name = "VerCol";
            this.VerCol.Text = "Colonias";
            this.VerCol.Width = 55;
            // 
            // pnlMap
            // 
            this.pnlMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMap.Controls.Add(this.panelCtrlMap);
            this.pnlMap.Controls.Add(this.sfmMainMap);
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(0, 50);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(494, 523);
            this.pnlMap.TabIndex = 34;
            // 
            // panelCtrlMap
            // 
            this.panelCtrlMap.BackColor = System.Drawing.Color.Transparent;
            this.panelCtrlMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCtrlMap.Controls.Add(this.pbbZoomMax);
            this.panelCtrlMap.Controls.Add(this.pbbZoomSelect);
            this.panelCtrlMap.Controls.Add(this.pbbZoomMin);
            this.panelCtrlMap.Controls.Add(this.trbZoom);
            this.panelCtrlMap.Location = new System.Drawing.Point(3, 4);
            this.panelCtrlMap.Name = "panelCtrlMap";
            this.panelCtrlMap.Size = new System.Drawing.Size(70, 116);
            this.panelCtrlMap.TabIndex = 1;
            // 
            // pbbZoomMax
            // 
            this.pbbZoomMax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbbZoomMax.Image = ((System.Drawing.Image)(resources.GetObject("pbbZoomMax.Image")));
            this.pbbZoomMax.Location = new System.Drawing.Point(33, 3);
            this.pbbZoomMax.Name = "pbbZoomMax";
            this.pbbZoomMax.Size = new System.Drawing.Size(32, 32);
            this.pbbZoomMax.TabIndex = 2;
            this.pbbZoomMax.TabStop = false;
            this.pbbZoomMax.Click += new System.EventHandler(this.pbbZoomMax_Click);
            this.pbbZoomMax.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbbZoomMin_MouseDown);
            this.pbbZoomMax.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbbZoomMin_MouseUp);
            // 
            // pbbZoomSelect
            // 
            this.pbbZoomSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbbZoomSelect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbbZoomSelect.Image = ((System.Drawing.Image)(resources.GetObject("pbbZoomSelect.Image")));
            this.pbbZoomSelect.Location = new System.Drawing.Point(33, 41);
            this.pbbZoomSelect.Name = "pbbZoomSelect";
            this.pbbZoomSelect.Size = new System.Drawing.Size(32, 32);
            this.pbbZoomSelect.TabIndex = 1;
            this.pbbZoomSelect.TabStop = false;
            this.pbbZoomSelect.Click += new System.EventHandler(this.pbbZoomSelect_Click);
            // 
            // pbbZoomMin
            // 
            this.pbbZoomMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbbZoomMin.Image = ((System.Drawing.Image)(resources.GetObject("pbbZoomMin.Image")));
            this.pbbZoomMin.Location = new System.Drawing.Point(33, 79);
            this.pbbZoomMin.Name = "pbbZoomMin";
            this.pbbZoomMin.Size = new System.Drawing.Size(32, 32);
            this.pbbZoomMin.TabIndex = 0;
            this.pbbZoomMin.TabStop = false;
            this.pbbZoomMin.Click += new System.EventHandler(this.pbbZoomMin_Click);
            this.pbbZoomMin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbbZoomMin_MouseDown);
            this.pbbZoomMin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbbZoomMin_MouseUp);
            // 
            // trbZoom
            // 
            this.trbZoom.LargeChange = 350;
            this.trbZoom.Location = new System.Drawing.Point(3, 1);
            this.trbZoom.Maximum = 500000;
            this.trbZoom.Minimum = 15;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbZoom.Size = new System.Drawing.Size(45, 110);
            this.trbZoom.SmallChange = 350;
            this.trbZoom.TabIndex = 3;
            this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbZoom.Value = 15;
            this.trbZoom.Scroll += new System.EventHandler(this.trbZoom_Scroll);
            this.trbZoom.ValueChanged += new System.EventHandler(this.trbZoom_ValueChanged);
            // 
            // sfmMainMap
            // 
            this.sfmMainMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sfmMainMap.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("sfmMainMap.CentrePoint2D")));
            this.sfmMainMap.CtrlDown = false;
            this.sfmMainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfmMainMap.Location = new System.Drawing.Point(0, 0);
            this.sfmMainMap.MapBackColor = System.Drawing.SystemColors.Control;
            this.sfmMainMap.Name = "sfmMainMap";
            this.sfmMainMap.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.sfmMainMap.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.sfmMainMap.Size = new System.Drawing.Size(490, 519);
            this.sfmMainMap.TabIndex = 0;
            this.sfmMainMap.UseMercatorProjection = false;
            this.sfmMainMap.ZoomLevel = 1D;
            this.sfmMainMap.ZoomLevelChanged += new System.EventHandler<System.EventArgs>(this.sfmMainMap_ZoomLevelChanged);
            this.sfmMainMap.SelectedRecordsChanged += new System.EventHandler<System.EventArgs>(this.sfmMainMap_SelectedRecordsChanged);
            this.sfmMainMap.OnControlKeyChange += new System.EventHandler(this.sfmMainMap_OnControlKeyChange);
            // 
            // PlazaAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 573);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.splitterTreeView);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlHead);
            this.Name = "PlazaAdmin";
            this.Text = "Administración de Plazas";
            this.pnlHead.ResumeLayout(false);
            this.pnlHeadFields.ResumeLayout(false);
            this.pnlHeadFields.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPlazas)).EndInit();
            this.pnlMap.ResumeLayout(false);
            this.panelCtrlMap.ResumeLayout(false);
            this.panelCtrlMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbbZoomMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlHeadFields;
        private System.Windows.Forms.Label label1;
        private NJFLib.Controls.CollapsibleSplitter splitterTreeView;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCanelZone;
        private System.Windows.Forms.Button btnSaveZone;
        private System.Windows.Forms.DataGridView dgPlazas;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Panel panelCtrlMap;
        private System.Windows.Forms.PictureBox pbbZoomMax;
        private System.Windows.Forms.PictureBox pbbZoomSelect;
        private System.Windows.Forms.PictureBox pbbZoomMin;
        private System.Windows.Forms.TrackBar trbZoom;
        private EGIS.Controls.SFMap sfmMainMap;
        private System.Windows.Forms.TextBox txtCurrentPlaza;
        private System.Windows.Forms.Label label2;
        private Clases.Controles.CheckComboBox ccbEstados;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMunicipio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plaza;
        private System.Windows.Forms.DataGridViewButtonColumn Editar;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.DataGridViewButtonColumn Ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewButtonColumn VerCol;

    }
}