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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            txtSourceFileName = new TextBox();
            btnPickSourceFile = new Button();
            label2 = new Label();
            txtSaveAsFileName = new TextBox();
            btnPickSaveAsFileName = new Button();
            btnSooperViewIt = new Button();
            prgProcess = new ProgressBar();
            btnCancel = new Button();
            ofdPickSourceFileName = new OpenFileDialog();
            openFileDialog1 = new OpenFileDialog();
            openFileDialog2 = new OpenFileDialog();
            ofdPickSaveAsFileName = new OpenFileDialog();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label3 = new Label();
            nudCRF = new NumericUpDown();
            label4 = new Label();
            cmbEncoding = new ComboBox();
            label5 = new Label();
            cmbHardware = new ComboBox();
            label6 = new Label();
            cmbColorspace = new ComboBox();
            groupBox1 = new GroupBox();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCRF).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = SystemColors.Control;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(txtSourceFileName);
            flowLayoutPanel1.Controls.Add(btnPickSourceFile);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(txtSaveAsFileName);
            flowLayoutPanel1.Controls.Add(btnPickSaveAsFileName);
            flowLayoutPanel1.Controls.Add(btnSooperViewIt);
            flowLayoutPanel1.Controls.Add(prgProcess);
            flowLayoutPanel1.Controls.Add(btnCancel);
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(330, 154);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(3, 0);
            label1.MinimumSize = new Size(53, 0);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 1;
            label1.Text = "Source:";
            // 
            // txtSourceFileName
            // 
            txtSourceFileName.Location = new Point(62, 3);
            txtSourceFileName.Name = "txtSourceFileName";
            txtSourceFileName.Size = new Size(225, 23);
            txtSourceFileName.TabIndex = 0;
            // 
            // btnPickSourceFile
            // 
            btnPickSourceFile.Location = new Point(293, 3);
            btnPickSourceFile.Name = "btnPickSourceFile";
            btnPickSourceFile.Size = new Size(31, 23);
            btnPickSourceFile.TabIndex = 6;
            btnPickSourceFile.Text = "...";
            btnPickSourceFile.UseVisualStyleBackColor = true;
            btnPickSourceFile.Click += btnPickSourceFile_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(3, 29);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 2;
            label2.Text = "Save As:";
            // 
            // txtSaveAsFileName
            // 
            txtSaveAsFileName.Location = new Point(62, 32);
            txtSaveAsFileName.Name = "txtSaveAsFileName";
            txtSaveAsFileName.Size = new Size(225, 23);
            txtSaveAsFileName.TabIndex = 3;
            // 
            // btnPickSaveAsFileName
            // 
            btnPickSaveAsFileName.Location = new Point(293, 32);
            btnPickSaveAsFileName.Name = "btnPickSaveAsFileName";
            btnPickSaveAsFileName.Size = new Size(31, 23);
            btnPickSaveAsFileName.TabIndex = 5;
            btnPickSaveAsFileName.Text = "...";
            btnPickSaveAsFileName.UseVisualStyleBackColor = true;
            btnPickSaveAsFileName.Click += btnPickSaveAsFileName_Click;
            // 
            // btnSooperViewIt
            // 
            btnSooperViewIt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSooperViewIt.Location = new Point(3, 61);
            btnSooperViewIt.Name = "btnSooperViewIt";
            btnSooperViewIt.Size = new Size(327, 23);
            btnSooperViewIt.TabIndex = 4;
            btnSooperViewIt.Text = "SooperView It!";
            btnSooperViewIt.UseVisualStyleBackColor = true;
            btnSooperViewIt.Click += btnSooperViewIt_Click;
            // 
            // prgProcess
            // 
            prgProcess.Location = new Point(3, 90);
            prgProcess.Name = "prgProcess";
            prgProcess.Size = new Size(327, 23);
            prgProcess.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Enabled = false;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(3, 119);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(327, 23);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // ofdPickSourceFileName
            // 
            ofdPickSourceFileName.AddToRecent = false;
            ofdPickSourceFileName.AutoUpgradeEnabled = false;
            ofdPickSourceFileName.FileName = "openFileDialog1";
            ofdPickSourceFileName.Title = "Select Source Video File";
            ofdPickSourceFileName.FileOk += ofdPickSourceFileName_FileOk;
            // 
            // openFileDialog1
            // 
            openFileDialog1.AddToRecent = false;
            openFileDialog1.AutoUpgradeEnabled = false;
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Title = "Select Source Video File";
            // 
            // openFileDialog2
            // 
            openFileDialog2.AddToRecent = false;
            openFileDialog2.AutoUpgradeEnabled = false;
            openFileDialog2.FileName = "openFileDialog1";
            openFileDialog2.Title = "Select Source Video File";
            // 
            // ofdPickSaveAsFileName
            // 
            ofdPickSaveAsFileName.CheckFileExists = false;
            ofdPickSaveAsFileName.Title = "Save As...";
            ofdPickSaveAsFileName.FileOk += ofdPickSaveAsFileName_FileOk;
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
            flowLayoutPanel2.Location = new Point(6, 22);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(318, 133);
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
            label4.Location = new Point(3, 29);
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
            cmbEncoding.Location = new Point(69, 32);
            cmbEncoding.Name = "cmbEncoding";
            cmbEncoding.Size = new Size(240, 23);
            cmbEncoding.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(3, 58);
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
            cmbHardware.Location = new Point(74, 61);
            cmbHardware.Name = "cmbHardware";
            cmbHardware.Size = new Size(233, 23);
            cmbHardware.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 87);
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
            cmbColorspace.Location = new Point(48, 90);
            cmbColorspace.Name = "cmbColorspace";
            cmbColorspace.Size = new Size(259, 23);
            cmbColorspace.TabIndex = 7;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(flowLayoutPanel2);
            groupBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            groupBox1.Location = new Point(12, 184);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(330, 161);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Options:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 357);
            Controls.Add(groupBox1);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "SooperView";
            FormClosed += Form1_FormClosed;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCRF).EndInit();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private TextBox txtSourceFileName;
        private Label label2;
        private TextBox txtSaveAsFileName;
        private Button btnSooperViewIt;
        private Button btnPickSourceFile;
        private Button btnPickSaveAsFileName;
        private ProgressBar prgProcess;
        private OpenFileDialog ofdPickSourceFileName;
        private OpenFileDialog openFileDialog1;
        private OpenFileDialog openFileDialog2;
        private OpenFileDialog ofdPickSaveAsFileName;
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
    }
}
