namespace SooperView
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnSooperViewIt = new Button();
            prgProcess = new ProgressBar();
            btnCancel = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label3 = new Label();
            nudCRF = new NumericUpDown();
            label4 = new Label();
            cmbEncoding = new ComboBox();
            label5 = new Label();
            cmbHardware = new ComboBox();
            label6 = new Label();
            cmbColorspace = new ComboBox();
            label1 = new Label();
            cmbTune = new ComboBox();
            label2 = new Label();
            cmbPreset = new ComboBox();
            label7 = new Label();
            cmbResolution = new ComboBox();
            groupBox1 = new GroupBox();
            lblVersion = new Label();
            lvFiles = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            lvLog = new ListView();
            columnHeader3 = new ColumnHeader();
            lblFileDrop = new Label();
            toolTip = new ToolTip(components);
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCRF).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.Control;
            flowLayoutPanel1.Controls.Add(btnSooperViewIt);
            flowLayoutPanel1.Controls.Add(prgProcess);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Location = new Point(11, 344);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(689, 96);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // btnSooperViewIt
            // 
            btnSooperViewIt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSooperViewIt.Location = new Point(3, 3);
            btnSooperViewIt.Name = "btnSooperViewIt";
            btnSooperViewIt.Size = new Size(679, 23);
            btnSooperViewIt.TabIndex = 4;
            btnSooperViewIt.Text = "SooperView It!";
            btnSooperViewIt.UseVisualStyleBackColor = true;
            btnSooperViewIt.Click += btnSooperViewIt_Click;
            // 
            // prgProcess
            // 
            prgProcess.Location = new Point(3, 32);
            prgProcess.Name = "prgProcess";
            prgProcess.Size = new Size(679, 23);
            prgProcess.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(3, 61);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(679, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = SystemColors.Control;
            flowLayoutPanel2.Controls.Add(label3);
            flowLayoutPanel2.Controls.Add(nudCRF);
            flowLayoutPanel2.Controls.Add(label4);
            flowLayoutPanel2.Controls.Add(cmbEncoding);
            flowLayoutPanel2.Controls.Add(label5);
            flowLayoutPanel2.Controls.Add(cmbHardware);
            flowLayoutPanel2.Controls.Add(label6);
            flowLayoutPanel2.Controls.Add(cmbColorspace);
            flowLayoutPanel2.Controls.Add(label1);
            flowLayoutPanel2.Controls.Add(cmbTune);
            flowLayoutPanel2.Controls.Add(label2);
            flowLayoutPanel2.Controls.Add(cmbPreset);
            flowLayoutPanel2.Controls.Add(label7);
            flowLayoutPanel2.Controls.Add(cmbResolution);
            flowLayoutPanel2.Location = new Point(6, 22);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(676, 123);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(158, 15);
            label3.TabIndex = 0;
            label3.Text = "CRF (Constant Rate Factor):";
            // 
            // nudCRF
            // 
            nudCRF.Location = new Point(167, 3);
            nudCRF.Maximum = new decimal(new int[] { 51, 0, 0, 0 });
            nudCRF.Name = "nudCRF";
            nudCRF.Size = new Size(141, 23);
            nudCRF.TabIndex = 1;
            nudCRF.Value = new decimal(new int[] { 18, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(314, 0);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 2;
            label4.Text = "Encoding:";
            // 
            // cmbEncoding
            // 
            cmbEncoding.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEncoding.FormattingEnabled = true;
            cmbEncoding.Items.AddRange(new object[] { "H264", "H265 (HEVC)", "AV1 (SVT-AV1)" });
            cmbEncoding.Location = new Point(380, 3);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new Size(285, 23);
            cmbEncoding.TabIndex = 3;
            cmbEncoding.SelectedIndexChanged += cmbEncoding_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(3, 29);
            label5.Name = "label5";
            label5.Size = new Size(65, 15);
            label5.TabIndex = 4;
            label5.Text = "Hardware:";
            // 
            // cmbHardware
            // 
            cmbHardware.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbHardware.FormattingEnabled = true;
            cmbHardware.Items.AddRange(new object[] { "CPU", "NVidia", "Intel", "AMD" });
            cmbHardware.Location = new Point(74, 32);
            cmbHardware.Name = "cmbHardware";
            cmbHardware.Size = new Size(233, 23);
            cmbHardware.TabIndex = 5;
            cmbHardware.SelectedIndexChanged += cmbHardware_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(313, 29);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 6;
            label6.Text = "Color:";
            // 
            // cmbColorspace
            // 
            cmbColorspace.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColorspace.FormattingEnabled = true;
            cmbColorspace.Items.AddRange(new object[] { "8-bit Colorspace", "10-bit Colorspace" });
            cmbColorspace.Location = new Point(358, 32);
            cmbColorspace.Name = "cmbColorspace";
            cmbColorspace.Size = new Size(307, 23);
            cmbColorspace.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 58);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 8;
            label1.Text = "Tune:";
            // 
            // cmbTune
            // 
            cmbTune.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTune.FormattingEnabled = true;
            cmbTune.Items.AddRange(new object[] { "None", "Film", "Grain", "Animation", "Still Image", "Fast Decode", "Zero Latency" });
            cmbTune.Location = new Point(46, 61);
            cmbTune.Name = "cmbTune";
            cmbTune.Size = new Size(261, 23);
            cmbTune.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(313, 58);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 10;
            label2.Text = "Preset:";
            // 
            // cmbPreset
            // 
            cmbPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPreset.FormattingEnabled = true;
            cmbPreset.Location = new Point(365, 61);
            cmbPreset.Name = "cmbPreset";
            cmbPreset.Size = new Size(300, 23);
            cmbPreset.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 87);
            label7.Name = "label7";
            label7.Size = new Size(112, 15);
            label7.TabIndex = 12;
            label7.Text = "Output Resolution:";
            // 
            // cmbResolution
            // 
            cmbResolution.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbResolution.FormattingEnabled = true;
            cmbResolution.Items.AddRange(new object[] { "3840x2160 (4k)", "2560x1440 (2k)", "1920x1080 (1080p)", "1280x720 (720p)" });
            cmbResolution.Location = new Point(121, 90);
            cmbResolution.Name = "cmbResolution";
            cmbResolution.Size = new Size(186, 23);
            cmbResolution.TabIndex = 13;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flowLayoutPanel2);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 184);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(688, 154);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options:";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(662, 549);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(37, 15);
            lblVersion.TabIndex = 3;
            lblVersion.Text = "v1.0.0";
            // 
            // lvFiles
            // 
            lvFiles.AllowDrop = true;
            lvFiles.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            lvFiles.FullRowSelect = true;
            lvFiles.GridLines = true;
            lvFiles.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvFiles.LabelWrap = false;
            lvFiles.Location = new Point(12, 20);
            lvFiles.Name = "lvFiles";
            lvFiles.OwnerDraw = true;
            lvFiles.Size = new Size(690, 158);
            lvFiles.TabIndex = 4;
            lvFiles.UseCompatibleStateImageBehavior = false;
            lvFiles.View = View.Details;
            lvFiles.DrawColumnHeader += listView_DrawColumnHeader;
            lvFiles.DrawItem += listView_DrawItem;
            lvFiles.DrawSubItem += listView_DrawSubItem;
            lvFiles.DragDrop += lvFiles_DragDrop;
            lvFiles.DragEnter += lvFiles_DragEnter;
            lvFiles.KeyDown += lvFiles_KeyDown;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Source";
            columnHeader1.Width = 345;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Save As";
            columnHeader2.Width = 345;
            // 
            // lvLog
            // 
            lvLog.Columns.AddRange(new ColumnHeader[] { columnHeader3 });
            lvLog.Location = new Point(11, 446);
            lvLog.Name = "lvLog";
            lvLog.OwnerDraw = true;
            lvLog.Size = new Size(690, 100);
            lvLog.TabIndex = 6;
            lvLog.UseCompatibleStateImageBehavior = false;
            lvLog.View = View.Details;
            lvLog.DrawColumnHeader += listView_DrawColumnHeader;
            lvLog.DrawItem += listView_DrawItem;
            lvLog.DrawSubItem += listView_DrawSubItem;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Log";
            columnHeader3.Width = 690;
            // 
            // lblFileDrop
            // 
            lblFileDrop.AllowDrop = true;
            lblFileDrop.BackColor = SystemColors.Window;
            lblFileDrop.BorderStyle = BorderStyle.FixedSingle;
            lblFileDrop.Location = new Point(12, 20);
            lblFileDrop.Name = "lblFileDrop";
            lblFileDrop.Size = new Size(690, 158);
            lblFileDrop.TabIndex = 7;
            lblFileDrop.Text = "DROP FILES HERE";
            lblFileDrop.TextAlign = ContentAlignment.MiddleCenter;
            lblFileDrop.DragDrop += lvFiles_DragDrop;
            lblFileDrop.DragEnter += lvFiles_DragEnter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(711, 573);
            Controls.Add(lblFileDrop);
            Controls.Add(lvLog);
            Controls.Add(lvFiles);
            Controls.Add(lblVersion);
            Controls.Add(groupBox1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "SooperView";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCRF).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnSooperViewIt;
        private ProgressBar prgProcess;
        private Button btnCancel;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label3;
        private NumericUpDown nudCRF;
        private Label label4;
        private ComboBox cmbEncoding;
        private Label label5;
        private ComboBox cmbHardware;
        private GroupBox groupBox1;
        private Label label6;
        private ComboBox cmbColorspace;
        private Label lblVersion;
        private ListView lvFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Label label1;
        private ComboBox cmbTune;
        private Label label2;
        private ComboBox cmbPreset;
        private Label label7;
        private ComboBox cmbResolution;
        private ListView lvLog;
        private ColumnHeader columnHeader3;
        private Label lblFileDrop;
        private ToolTip toolTip;
    }
}
