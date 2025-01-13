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
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 177);
            Controls.Add(flowLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "SooperView";
            FormClosed += Form1_FormClosed;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
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
    }
}
