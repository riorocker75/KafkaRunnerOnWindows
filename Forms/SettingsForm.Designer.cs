namespace kafkaRunnerWindows.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblMsys;
        private System.Windows.Forms.TextBox txtMsysBin;
        private System.Windows.Forms.Button btnBrowseMsys;
        private System.Windows.Forms.Label lblGcc;
        private System.Windows.Forms.TextBox txtGcc;
        private System.Windows.Forms.Button btnBrowseGcc;
        private System.Windows.Forms.Label lblCommand;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.CheckBox chkDiagnostics;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlBottom;

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblMsys = new System.Windows.Forms.Label();
            this.txtMsysBin = new System.Windows.Forms.TextBox();
            this.btnBrowseMsys = new System.Windows.Forms.Button();
            this.lblGcc = new System.Windows.Forms.Label();
            this.txtGcc = new System.Windows.Forms.TextBox();
            this.btnBrowseGcc = new System.Windows.Forms.Button();
            this.lblCommand = new System.Windows.Forms.Label();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.chkDiagnostics = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.Location = new System.Drawing.Point(18, 16);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(225, 20);
            this.lblHeader.Text = "Build & Environment Settings";
            // 
            // lblMsys
            // 
            this.lblMsys.AutoSize = true;
            this.lblMsys.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMsys.Location = new System.Drawing.Point(20, 52);
            this.lblMsys.Name = "lblMsys";
            this.lblMsys.Size = new System.Drawing.Size(127, 15);
            this.lblMsys.Text = "MSYS2 MinGW Bin Path:";
            // 
            // txtMsysBin
            // 
            this.txtMsysBin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMsysBin.Location = new System.Drawing.Point(20, 72);
            this.txtMsysBin.Name = "txtMsysBin";
            this.txtMsysBin.Size = new System.Drawing.Size(430, 23);
            // 
            // btnBrowseMsys
            // 
            this.btnBrowseMsys.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseMsys.Location = new System.Drawing.Point(458, 71);
            this.btnBrowseMsys.Name = "btnBrowseMsys";
            this.btnBrowseMsys.Size = new System.Drawing.Size(85, 25);
            this.btnBrowseMsys.Text = "Browse...";
            this.btnBrowseMsys.UseVisualStyleBackColor = true;
            this.btnBrowseMsys.Click += new System.EventHandler(this.btnBrowseMsys_Click);
            // 
            // lblGcc
            // 
            this.lblGcc.AutoSize = true;
            this.lblGcc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblGcc.Location = new System.Drawing.Point(20, 108);
            this.lblGcc.Name = "lblGcc";
            this.lblGcc.Size = new System.Drawing.Size(84, 15);
            this.lblGcc.Text = "GCC.EXE Path:";
            // 
            // txtGcc
            // 
            this.txtGcc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGcc.Location = new System.Drawing.Point(20, 128);
            this.txtGcc.Name = "txtGcc";
            this.txtGcc.Size = new System.Drawing.Size(430, 23);
            // 
            // btnBrowseGcc
            // 
            this.btnBrowseGcc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseGcc.Location = new System.Drawing.Point(458, 127);
            this.btnBrowseGcc.Name = "btnBrowseGcc";
            this.btnBrowseGcc.Size = new System.Drawing.Size(85, 25);
            this.btnBrowseGcc.Text = "Browse...";
            this.btnBrowseGcc.UseVisualStyleBackColor = true;
            this.btnBrowseGcc.Click += new System.EventHandler(this.btnBrowseGcc_Click);
            // 
            // lblCommand
            // 
            this.lblCommand.AutoSize = true;
            this.lblCommand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCommand.Location = new System.Drawing.Point(20, 165);
            this.lblCommand.Name = "lblCommand";
            this.lblCommand.Size = new System.Drawing.Size(126, 15);
            this.lblCommand.Text = "Default Run Command:";
            // 
            // txtCommand
            // 
            this.txtCommand.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCommand.Location = new System.Drawing.Point(20, 185);
            this.txtCommand.Name = "txtCommand";
            this.txtCommand.Size = new System.Drawing.Size(523, 23);
            // 
            // chkDiagnostics
            // 
            this.chkDiagnostics.AutoSize = true;
            this.chkDiagnostics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkDiagnostics.Location = new System.Drawing.Point(20, 222);
            this.chkDiagnostics.Name = "chkDiagnostics";
            this.chkDiagnostics.Size = new System.Drawing.Size(325, 19);
            this.chkDiagnostics.Text = "Show Go environment & GCC diagnostics before running";
            this.chkDiagnostics.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 260);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(564, 50);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(365, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(463, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 28);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(564, 310);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.chkDiagnostics);
            this.Controls.Add(this.txtCommand);
            this.Controls.Add(this.lblCommand);
            this.Controls.Add(this.btnBrowseGcc);
            this.Controls.Add(this.txtGcc);
            this.Controls.Add(this.lblGcc);
            this.Controls.Add(this.btnBrowseMsys);
            this.Controls.Add(this.txtMsysBin);
            this.Controls.Add(this.lblMsys);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Runner Settings";
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
