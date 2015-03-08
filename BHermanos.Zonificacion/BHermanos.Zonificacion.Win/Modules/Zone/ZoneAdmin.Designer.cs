
namespace BHermanos.Zonificacion.Win.Modules.Zone
{
    partial class ZoneAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoneAdmin));
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlHeadFields = new System.Windows.Forms.Panel();
            this.txtCurrentSubzona = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurrentZona = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMunicipio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitterTreeView = new NJFLib.Controls.CollapsibleSplitter();
            this.tabInfo = new System.Windows.Forms.TabControl();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnNSE = new System.Windows.Forms.Button();
            this.btnViewAll = new System.Windows.Forms.Button();
            this.dgvReportZonas = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNewZubZona = new System.Windows.Forms.Button();
            this.btnCanelZone = new System.Windows.Forms.Button();
            this.btnSaveZone = new System.Windows.Forms.Button();
            this.dgZone = new System.Windows.Forms.DataGridView();
            this.tabPageZonas = new System.Windows.Forms.TabPage();
            this.sgReportZoneSingle = new System.Windows.Forms.DataGridView();
            this.lblCurrentZonaName = new System.Windows.Forms.Label();
            this.dgListZones = new System.Windows.Forms.DataGridView();
            this.Zona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ver = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ZoneId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlMap = new System.Windows.Forms.Panel();
            this.panelCtrlMap = new System.Windows.Forms.Panel();
            this.pbbZoomMax = new System.Windows.Forms.PictureBox();
            this.pbbZoomSelect = new System.Windows.Forms.PictureBox();
            this.pbbZoomMin = new System.Windows.Forms.PictureBox();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.sfmMainMap = new EGIS.Controls.SFMap();
            this.pnlHead.SuspendLayout();
            this.pnlHeadFields.SuspendLayout();
            this.tabInfo.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportZonas)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgZone)).BeginInit();
            this.tabPageZonas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sgReportZoneSingle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgListZones)).BeginInit();
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
            this.pnlHead.TabIndex = 1;
            // 
            // pnlHeadFields
            // 
            this.pnlHeadFields.Controls.Add(this.txtCurrentSubzona);
            this.pnlHeadFields.Controls.Add(this.label4);
            this.pnlHeadFields.Controls.Add(this.txtCurrentZona);
            this.pnlHeadFields.Controls.Add(this.label3);
            this.pnlHeadFields.Controls.Add(this.cmbEstado);
            this.pnlHeadFields.Controls.Add(this.label2);
            this.pnlHeadFields.Controls.Add(this.cmbMunicipio);
            this.pnlHeadFields.Controls.Add(this.label1);
            this.pnlHeadFields.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlHeadFields.Location = new System.Drawing.Point(385, 0);
            this.pnlHeadFields.Name = "pnlHeadFields";
            this.pnlHeadFields.Size = new System.Drawing.Size(425, 48);
            this.pnlHeadFields.TabIndex = 4;
            // 
            // txtCurrentSubzona
            // 
            this.txtCurrentSubzona.Location = new System.Drawing.Point(583, 22);
            this.txtCurrentSubzona.Name = "txtCurrentSubzona";
            this.txtCurrentSubzona.ReadOnly = true;
            this.txtCurrentSubzona.Size = new System.Drawing.Size(150, 20);
            this.txtCurrentSubzona.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(580, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subzona:";
            // 
            // txtCurrentZona
            // 
            this.txtCurrentZona.Location = new System.Drawing.Point(427, 22);
            this.txtCurrentZona.Name = "txtCurrentZona";
            this.txtCurrentZona.ReadOnly = true;
            this.txtCurrentZona.Size = new System.Drawing.Size(150, 20);
            this.txtCurrentZona.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(424, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Zona:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(15, 22);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(200, 21);
            this.cmbEstado.TabIndex = 0;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Municipio:";
            // 
            // cmbMunicipio
            // 
            this.cmbMunicipio.FormattingEnabled = true;
            this.cmbMunicipio.Location = new System.Drawing.Point(221, 22);
            this.cmbMunicipio.Name = "cmbMunicipio";
            this.cmbMunicipio.Size = new System.Drawing.Size(200, 21);
            this.cmbMunicipio.TabIndex = 1;
            this.cmbMunicipio.SelectedIndexChanged += new System.EventHandler(this.cmbMunicipio_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Estado:";
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(812, 5);
            this.panel2.TabIndex = 2;
            // 
            // splitterTreeView
            // 
            this.splitterTreeView.AnimationDelay = 20;
            this.splitterTreeView.AnimationStep = 20;
            this.splitterTreeView.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.splitterTreeView.ControlToHide = this.tabInfo;
            this.splitterTreeView.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterTreeView.ExpandParentForm = true;
            this.splitterTreeView.Location = new System.Drawing.Point(454, 55);
            this.splitterTreeView.Name = "splitterTreeView";
            this.splitterTreeView.TabIndex = 31;
            this.splitterTreeView.TabStop = false;
            this.splitterTreeView.UseAnimations = false;
            this.splitterTreeView.VisualStyle = NJFLib.Controls.VisualStyles.DoubleDots;
            // 
            // tabInfo
            // 
            this.tabInfo.Controls.Add(this.tabPageInfo);
            this.tabInfo.Controls.Add(this.tabPageZonas);
            this.tabInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabInfo.Location = new System.Drawing.Point(462, 55);
            this.tabInfo.Name = "tabInfo";
            this.tabInfo.SelectedIndex = 0;
            this.tabInfo.Size = new System.Drawing.Size(350, 518);
            this.tabInfo.TabIndex = 30;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.panel3);
            this.tabPageInfo.Controls.Add(this.dgvReportZonas);
            this.tabPageInfo.Controls.Add(this.panel1);
            this.tabPageInfo.Controls.Add(this.dgZone);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInfo.Size = new System.Drawing.Size(342, 492);
            this.tabPageInfo.TabIndex = 0;
            this.tabPageInfo.Text = "Creación";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnNSE);
            this.panel3.Controls.Add(this.btnViewAll);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 448);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(336, 41);
            this.panel3.TabIndex = 3;
            // 
            // btnNSE
            // 
            this.btnNSE.Location = new System.Drawing.Point(233, 6);
            this.btnNSE.Name = "btnNSE";
            this.btnNSE.Size = new System.Drawing.Size(100, 29);
            this.btnNSE.TabIndex = 5;
            this.btnNSE.Text = "NSE";
            this.btnNSE.UseVisualStyleBackColor = true;
            this.btnNSE.Click += new System.EventHandler(this.btnNSE_Click);
            // 
            // btnViewAll
            // 
            this.btnViewAll.Location = new System.Drawing.Point(127, 6);
            this.btnViewAll.Name = "btnViewAll";
            this.btnViewAll.Size = new System.Drawing.Size(100, 29);
            this.btnViewAll.TabIndex = 4;
            this.btnViewAll.Text = "Ver Completo";
            this.btnViewAll.UseVisualStyleBackColor = true;
            this.btnViewAll.Click += new System.EventHandler(this.btnViewAll_Click);
            // 
            // dgvReportZonas
            // 
            this.dgvReportZonas.AllowUserToAddRows = false;
            this.dgvReportZonas.AllowUserToDeleteRows = false;
            this.dgvReportZonas.AllowUserToResizeRows = false;
            this.dgvReportZonas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvReportZonas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReportZonas.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvReportZonas.Location = new System.Drawing.Point(3, 258);
            this.dgvReportZonas.MultiSelect = false;
            this.dgvReportZonas.Name = "dgvReportZonas";
            this.dgvReportZonas.ReadOnly = true;
            this.dgvReportZonas.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvReportZonas.RowHeadersVisible = false;
            this.dgvReportZonas.Size = new System.Drawing.Size(336, 190);
            this.dgvReportZonas.TabIndex = 2;
            this.dgvReportZonas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReportZonas_CellClick);
            this.dgvReportZonas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvReportZonas_DataBindingComplete);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnNewZubZona);
            this.panel1.Controls.Add(this.btnCanelZone);
            this.panel1.Controls.Add(this.btnSaveZone);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 193);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 65);
            this.panel1.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(233, 33);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 29);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Eliminar Zona";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNewZubZona
            // 
            this.btnNewZubZona.Location = new System.Drawing.Point(233, 3);
            this.btnNewZubZona.Name = "btnNewZubZona";
            this.btnNewZubZona.Size = new System.Drawing.Size(100, 29);
            this.btnNewZubZona.TabIndex = 6;
            this.btnNewZubZona.Text = "Nueva Subzona";
            this.btnNewZubZona.UseVisualStyleBackColor = true;
            this.btnNewZubZona.Click += new System.EventHandler(this.btnNewZubZona_Click);
            // 
            // btnCanelZone
            // 
            this.btnCanelZone.Location = new System.Drawing.Point(128, 3);
            this.btnCanelZone.Name = "btnCanelZone";
            this.btnCanelZone.Size = new System.Drawing.Size(100, 29);
            this.btnCanelZone.TabIndex = 5;
            this.btnCanelZone.Text = "Cancelar";
            this.btnCanelZone.UseVisualStyleBackColor = true;
            this.btnCanelZone.Click += new System.EventHandler(this.btnCanelZone_Click);
            // 
            // btnSaveZone
            // 
            this.btnSaveZone.Location = new System.Drawing.Point(22, 3);
            this.btnSaveZone.Name = "btnSaveZone";
            this.btnSaveZone.Size = new System.Drawing.Size(100, 29);
            this.btnSaveZone.TabIndex = 4;
            this.btnSaveZone.Text = "Aceptar";
            this.btnSaveZone.UseVisualStyleBackColor = true;
            this.btnSaveZone.Click += new System.EventHandler(this.btnCreteZone_Click);
            // 
            // dgZone
            // 
            this.dgZone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgZone.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgZone.Location = new System.Drawing.Point(3, 3);
            this.dgZone.Name = "dgZone";
            this.dgZone.RowHeadersVisible = false;
            this.dgZone.Size = new System.Drawing.Size(336, 190);
            this.dgZone.TabIndex = 0;
            // 
            // tabPageZonas
            // 
            this.tabPageZonas.Controls.Add(this.sgReportZoneSingle);
            this.tabPageZonas.Controls.Add(this.lblCurrentZonaName);
            this.tabPageZonas.Controls.Add(this.dgListZones);
            this.tabPageZonas.Location = new System.Drawing.Point(4, 22);
            this.tabPageZonas.Name = "tabPageZonas";
            this.tabPageZonas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageZonas.Size = new System.Drawing.Size(342, 492);
            this.tabPageZonas.TabIndex = 1;
            this.tabPageZonas.Text = "Por zona";
            this.tabPageZonas.UseVisualStyleBackColor = true;
            // 
            // sgReportZoneSingle
            // 
            this.sgReportZoneSingle.AllowUserToAddRows = false;
            this.sgReportZoneSingle.AllowUserToDeleteRows = false;
            this.sgReportZoneSingle.AllowUserToResizeColumns = false;
            this.sgReportZoneSingle.AllowUserToResizeRows = false;
            this.sgReportZoneSingle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sgReportZoneSingle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sgReportZoneSingle.Location = new System.Drawing.Point(3, 251);
            this.sgReportZoneSingle.Name = "sgReportZoneSingle";
            this.sgReportZoneSingle.RowHeadersVisible = false;
            this.sgReportZoneSingle.Size = new System.Drawing.Size(336, 238);
            this.sgReportZoneSingle.TabIndex = 3;
            // 
            // lblCurrentZonaName
            // 
            this.lblCurrentZonaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentZonaName.AutoSize = true;
            this.lblCurrentZonaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentZonaName.Location = new System.Drawing.Point(0, 222);
            this.lblCurrentZonaName.Name = "lblCurrentZonaName";
            this.lblCurrentZonaName.Size = new System.Drawing.Size(75, 24);
            this.lblCurrentZonaName.TabIndex = 2;
            this.lblCurrentZonaName.Text = "Design";
            this.lblCurrentZonaName.TextChanged += new System.EventHandler(this.lblCurrentZonaName_TextChanged);
            // 
            // dgListZones
            // 
            this.dgListZones.AllowUserToAddRows = false;
            this.dgListZones.AllowUserToDeleteRows = false;
            this.dgListZones.AllowUserToResizeColumns = false;
            this.dgListZones.AllowUserToResizeRows = false;
            this.dgListZones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgListZones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zona,
            this.Ver,
            this.ZoneId});
            this.dgListZones.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgListZones.Location = new System.Drawing.Point(3, 3);
            this.dgListZones.Name = "dgListZones";
            this.dgListZones.RowHeadersVisible = false;
            this.dgListZones.Size = new System.Drawing.Size(336, 197);
            this.dgListZones.TabIndex = 1;
            this.dgListZones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgListZones_CellClick);
            // 
            // Zona
            // 
            this.Zona.DataPropertyName = "Zona";
            this.Zona.Frozen = true;
            this.Zona.HeaderText = "Zona";
            this.Zona.Name = "Zona";
            this.Zona.ReadOnly = true;
            this.Zona.Width = 150;
            // 
            // Ver
            // 
            this.Ver.DataPropertyName = "Ver";
            this.Ver.HeaderText = "";
            this.Ver.Name = "Ver";
            // 
            // ZoneId
            // 
            this.ZoneId.DataPropertyName = "ZoneId";
            this.ZoneId.HeaderText = "ZoneId";
            this.ZoneId.Name = "ZoneId";
            this.ZoneId.Visible = false;
            // 
            // pnlMap
            // 
            this.pnlMap.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMap.Controls.Add(this.panelCtrlMap);
            this.pnlMap.Controls.Add(this.sfmMainMap);
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(0, 55);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(454, 518);
            this.pnlMap.TabIndex = 33;
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
            this.trbZoom.LargeChange = 20;
            this.trbZoom.Location = new System.Drawing.Point(3, 1);
            this.trbZoom.Maximum = 500000;
            this.trbZoom.Minimum = 15;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbZoom.Size = new System.Drawing.Size(45, 110);
            this.trbZoom.SmallChange = 20;
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
            this.sfmMainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfmMainMap.Location = new System.Drawing.Point(0, 0);
            this.sfmMainMap.MapBackColor = System.Drawing.SystemColors.Control;
            this.sfmMainMap.Name = "sfmMainMap";
            this.sfmMainMap.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.sfmMainMap.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.sfmMainMap.Size = new System.Drawing.Size(450, 514);
            this.sfmMainMap.TabIndex = 0;
            this.sfmMainMap.UseMercatorProjection = false;
            this.sfmMainMap.ZoomLevel = 1D;
            this.sfmMainMap.ZoomLevelChanged += new System.EventHandler<System.EventArgs>(this.sfmMainMap_ZoomLevelChanged);
            this.sfmMainMap.SelectedRecordsChanged += new System.EventHandler<System.EventArgs>(this.sfmMainMap_SelectedRecordsChanged);
            this.sfmMainMap.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sfmMainMap_KeyUp);
            // 
            // ZoneAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 573);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.splitterTreeView);
            this.Controls.Add(this.tabInfo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHead);
            this.Name = "ZoneAdmin";
            this.Text = "Administración de Zonas";
            this.Resize += new System.EventHandler(this.ZoneCreatorForm_Resize);
            this.pnlHead.ResumeLayout(false);
            this.pnlHeadFields.ResumeLayout(false);
            this.pnlHeadFields.PerformLayout();
            this.tabInfo.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportZonas)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgZone)).EndInit();
            this.tabPageZonas.ResumeLayout(false);
            this.tabPageZonas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sgReportZoneSingle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgListZones)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMunicipio;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Panel panel2;
        private NJFLib.Controls.CollapsibleSplitter splitterTreeView;
        private System.Windows.Forms.Panel pnlMap;
        private System.Windows.Forms.Panel pnlHeadFields;
        private EGIS.Controls.SFMap sfmMainMap;
        private System.Windows.Forms.TextBox txtCurrentZona;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCurrentSubzona;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbbZoomMin;
        private System.Windows.Forms.PictureBox pbbZoomMax;
        private System.Windows.Forms.PictureBox pbbZoomSelect;
        private System.Windows.Forms.TrackBar trbZoom;
        private System.Windows.Forms.Panel panelCtrlMap;
        private System.Windows.Forms.TabControl tabInfo;
        private System.Windows.Forms.TabPage tabPageInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnNSE;
        private System.Windows.Forms.Button btnViewAll;
        private System.Windows.Forms.DataGridView dgvReportZonas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNewZubZona;
        private System.Windows.Forms.Button btnCanelZone;
        private System.Windows.Forms.Button btnSaveZone;
        private System.Windows.Forms.DataGridView dgZone;
        private System.Windows.Forms.TabPage tabPageZonas;
        private System.Windows.Forms.DataGridView sgReportZoneSingle;
        private System.Windows.Forms.Label lblCurrentZonaName;
        private System.Windows.Forms.DataGridView dgListZones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zona;
        private System.Windows.Forms.DataGridViewButtonColumn Ver;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZoneId;
        private System.Windows.Forms.Button btnDelete;
    }
}