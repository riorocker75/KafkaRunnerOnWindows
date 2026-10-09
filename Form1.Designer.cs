namespace kafkaRunnerWindows
{
    partial class Form1
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

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblAppSubtitle;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.Panel pnlAddProject;
        private System.Windows.Forms.Label lblPathPrompt;
        private System.Windows.Forms.TextBox txtProjectPath;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnAddProject;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlTableToolbar;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnForceKill;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.DataGridView dgvProjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFeatureTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWorkStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastRun;
        private System.Windows.Forms.Panel pnlConsoleHeader;
        private System.Windows.Forms.Label lblConsoleTitle;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnCopyLog;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.ContextMenuStrip cmsProject;
        private System.Windows.Forms.ToolStripMenuItem miRun;
        private System.Windows.Forms.ToolStripMenuItem miStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miOpenExplorer;
        private System.Windows.Forms.ToolStripMenuItem miOpenCmd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miRemove;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblCurrentStatus = new System.Windows.Forms.Label();
            this.lblAppSubtitle = new System.Windows.Forms.Label();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.pnlAddProject = new System.Windows.Forms.Panel();
            this.btnAddProject = new System.Windows.Forms.Button();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.txtProjectPath = new System.Windows.Forms.TextBox();
            this.lblPathPrompt = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgvProjects = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFeatureTask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWorkStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRun = new System.Windows.Forms.ToolStripMenuItem();
            this.miStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenCmd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.miRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlTableToolbar = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnForceKill = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.pnlConsoleHeader = new System.Windows.Forms.Panel();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnCopyLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lblConsoleTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlAddProject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
            this.cmsProject.SuspendLayout();
            this.pnlTableToolbar.SuspendLayout();
            this.pnlConsoleHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(24, 28, 36);
            this.pnlHeader.Controls.Add(this.lblCurrentStatus);
            this.pnlHeader.Controls.Add(this.lblAppSubtitle);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1080, 60);
            // 
            // lblCurrentStatus
            // 
            this.lblCurrentStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentStatus.BackColor = System.Drawing.Color.FromArgb(40, 48, 60);
            this.lblCurrentStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentStatus.ForeColor = System.Drawing.Color.FromArgb(180, 195, 215);
            this.lblCurrentStatus.Location = new System.Drawing.Point(820, 15);
            this.lblCurrentStatus.Name = "lblCurrentStatus";
            this.lblCurrentStatus.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblCurrentStatus.Size = new System.Drawing.Size(245, 30);
            this.lblCurrentStatus.Text = "Status: Idle";
            this.lblCurrentStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAppSubtitle
            // 
            this.lblAppSubtitle.AutoSize = true;
            this.lblAppSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAppSubtitle.ForeColor = System.Drawing.Color.FromArgb(150, 160, 175);
            this.lblAppSubtitle.Location = new System.Drawing.Point(16, 35);
            this.lblAppSubtitle.Name = "lblAppSubtitle";
            this.lblAppSubtitle.Size = new System.Drawing.Size(350, 13);
            this.lblAppSubtitle.Text = "Manage and execute Kafka Go Microservices with MinGW CGO Runner";
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(15, 9);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(235, 23);
            this.lblAppTitle.Text = "Kafka & Go Service Runner";
            // 
            // pnlAddProject
            // 
            this.pnlAddProject.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlAddProject.Controls.Add(this.btnAddProject);
            this.pnlAddProject.Controls.Add(this.btnBrowseFolder);
            this.pnlAddProject.Controls.Add(this.txtProjectPath);
            this.pnlAddProject.Controls.Add(this.lblPathPrompt);
            this.pnlAddProject.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddProject.Location = new System.Drawing.Point(0, 60);
            this.pnlAddProject.Name = "pnlAddProject";
            this.pnlAddProject.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlAddProject.Size = new System.Drawing.Size(1080, 52);
            // 
            // btnAddProject
            // 
            this.btnAddProject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddProject.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.btnAddProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnAddProject.ForeColor = System.Drawing.Color.White;
            this.btnAddProject.Location = new System.Drawing.Point(950, 10);
            this.btnAddProject.Name = "btnAddProject";
            this.btnAddProject.Size = new System.Drawing.Size(118, 30);
            this.btnAddProject.Text = "+ Add Project";
            this.btnAddProject.UseVisualStyleBackColor = false;
            this.btnAddProject.Click += new System.EventHandler(this.btnAddProject_Click);
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBrowseFolder.Location = new System.Drawing.Point(834, 10);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(108, 30);
            this.btnBrowseFolder.Text = "📁 Browse...";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            // 
            // txtProjectPath
            // 
            this.txtProjectPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjectPath.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtProjectPath.Location = new System.Drawing.Point(135, 13);
            this.txtProjectPath.Name = "txtProjectPath";
            this.txtProjectPath.PlaceholderText = "Paste or select project folder path (e.g., D:\\Projects\\utility-service)...";
            this.txtProjectPath.Size = new System.Drawing.Size(690, 25);
            this.txtProjectPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProjectPath_KeyDown);
            // 
            // lblPathPrompt
            // 
            this.lblPathPrompt.AutoSize = true;
            this.lblPathPrompt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPathPrompt.Location = new System.Drawing.Point(15, 17);
            this.lblPathPrompt.Name = "lblPathPrompt";
            this.lblPathPrompt.Size = new System.Drawing.Size(100, 15);
            this.lblPathPrompt.Text = "Project Directory:";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 112);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.dgvProjects);
            this.splitMain.Panel1.Controls.Add(this.pnlTableToolbar);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.rtbOutput);
            this.splitMain.Panel2.Controls.Add(this.pnlConsoleHeader);
            this.splitMain.Size = new System.Drawing.Size(1080, 588);
            this.splitMain.SplitterDistance = 280;
            // 
            // dgvProjects
            // 
            this.dgvProjects.AllowUserToAddRows = false;
            this.dgvProjects.AllowUserToDeleteRows = false;
            this.dgvProjects.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dgvProjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjects.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProjects.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProjects.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(238, 242, 246);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.dgvProjects.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProjects.ColumnHeadersHeight = 34;
            this.dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colFeatureTask,
            this.colWorkStatus,
            this.colStatus,
            this.colPath,
            this.colLastRun});
            this.dgvProjects.ContextMenuStrip = this.cmsProject;
            this.dgvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProjects.EnableHeadersVisualStyles = false;
            this.dgvProjects.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dgvProjects.Location = new System.Drawing.Point(0, 42);
            this.dgvProjects.MultiSelect = false;
            this.dgvProjects.Name = "dgvProjects";
            this.dgvProjects.ReadOnly = true;
            this.dgvProjects.RowHeadersVisible = false;
            this.dgvProjects.RowTemplate.Height = 32;
            this.dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjects.Size = new System.Drawing.Size(1080, 238);
            this.dgvProjects.SelectionChanged += new System.EventHandler(this.dgvProjects_SelectionChanged);
            this.dgvProjects.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProjects_CellDoubleClick);
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Project Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 160;
            // 
            // colFeatureTask
            // 
            this.colFeatureTask.DataPropertyName = "FeatureTask";
            this.colFeatureTask.HeaderText = "Fitur / Tugas Dikerjakan";
            this.colFeatureTask.Name = "colFeatureTask";
            this.colFeatureTask.ReadOnly = true;
            this.colFeatureTask.Width = 240;
            // 
            // colWorkStatus
            // 
            this.colWorkStatus.DataPropertyName = "WorkStatus";
            this.colWorkStatus.HeaderText = "Status Tugas";
            this.colWorkStatus.Name = "colWorkStatus";
            this.colWorkStatus.ReadOnly = true;
            this.colWorkStatus.Width = 120;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.HeaderText = "Runner Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 110;
            // 
            // colPath
            // 
            this.colPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPath.DataPropertyName = "Path";
            this.colPath.HeaderText = "Directory Path";
            this.colPath.Name = "colPath";
            this.colPath.ReadOnly = true;
            // 
            // colLastRun
            // 
            this.colLastRun.HeaderText = "Last Run";
            this.colLastRun.Name = "colLastRun";
            this.colLastRun.ReadOnly = true;
            this.colLastRun.Width = 150;
            // 
            // cmsProject
            // 
            this.cmsProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRun,
            this.miStop,
            this.toolStripSeparator1,
            this.miEdit,
            this.miOpenExplorer,
            this.miOpenCmd,
            this.toolStripSeparator2,
            this.miRemove});
            this.cmsProject.Name = "cmsProject";
            this.cmsProject.Size = new System.Drawing.Size(235, 148);
            // 
            // miRun
            // 
            this.miRun.Name = "miRun";
            this.miRun.Size = new System.Drawing.Size(234, 22);
            this.miRun.Text = "▶ Run Project";
            this.miRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // miStop
            // 
            this.miStop.Name = "miStop";
            this.miStop.Size = new System.Drawing.Size(234, 22);
            this.miStop.Text = "⏹ Stop Runner";
            this.miStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // miEdit
            // 
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(234, 22);
            this.miEdit.Text = "✏️ Edit Info Fitur & Status...";
            this.miEdit.Click += new System.EventHandler(this.miEdit_Click);
            // 
            // miOpenExplorer
            // 
            this.miOpenExplorer.Name = "miOpenExplorer";
            this.miOpenExplorer.Size = new System.Drawing.Size(234, 22);
            this.miOpenExplorer.Text = "📂 Open in File Explorer";
            this.miOpenExplorer.Click += new System.EventHandler(this.miOpenExplorer_Click);
            // 
            // miOpenCmd
            // 
            this.miOpenCmd.Name = "miOpenCmd";
            this.miOpenCmd.Size = new System.Drawing.Size(234, 22);
            this.miOpenCmd.Text = "💻 Open Command Prompt";
            this.miOpenCmd.Click += new System.EventHandler(this.miOpenCmd_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // miRemove
            // 
            this.miRemove.Name = "miRemove";
            this.miRemove.Size = new System.Drawing.Size(234, 22);
            this.miRemove.Text = "🗑 Remove from List";
            this.miRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // pnlTableToolbar
            // 
            this.pnlTableToolbar.BackColor = System.Drawing.Color.White;
            this.pnlTableToolbar.Controls.Add(this.btnSettings);
            this.pnlTableToolbar.Controls.Add(this.btnImport);
            this.pnlTableToolbar.Controls.Add(this.btnRefresh);
            this.pnlTableToolbar.Controls.Add(this.btnRemove);
            this.pnlTableToolbar.Controls.Add(this.btnForceKill);
            this.pnlTableToolbar.Controls.Add(this.btnStop);
            this.pnlTableToolbar.Controls.Add(this.btnRun);
            this.pnlTableToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlTableToolbar.Name = "pnlTableToolbar";
            this.pnlTableToolbar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlTableToolbar.Size = new System.Drawing.Size(1080, 42);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSettings.Location = new System.Drawing.Point(976, 6);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(94, 30);
            this.btnSettings.Text = "⚙ Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnImport.Location = new System.Drawing.Point(874, 6);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(95, 30);
            this.btnImport.Text = "📥 Import...";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRefresh.Location = new System.Drawing.Point(424, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(115, 30);
            this.btnRefresh.Text = "🔄 Check Status";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRemove.Location = new System.Drawing.Point(324, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(95, 30);
            this.btnRemove.Text = "🗑 Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnForceKill
            // 
            this.btnForceKill.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnForceKill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForceKill.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnForceKill.ForeColor = System.Drawing.Color.White;
            this.btnForceKill.Location = new System.Drawing.Point(202, 6);
            this.btnForceKill.Name = "btnForceKill";
            this.btnForceKill.Size = new System.Drawing.Size(116, 30);
            this.btnForceKill.Text = "⚡ Kill Stray";
            this.btnForceKill.UseVisualStyleBackColor = false;
            this.btnForceKill.Click += new System.EventHandler(this.btnForceKill_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(106, 6);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(90, 30);
            this.btnStop.Text = "⏹ Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(25, 135, 84);
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(10, 6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(90, 30);
            this.btnRun.Text = "▶ Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // rtbOutput
            // 
            this.rtbOutput.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rtbOutput.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            this.rtbOutput.Location = new System.Drawing.Point(0, 36);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(1080, 268);
            this.rtbOutput.TabIndex = 1;
            this.rtbOutput.Text = "";
            // 
            // pnlConsoleHeader
            // 
            this.pnlConsoleHeader.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.pnlConsoleHeader.Controls.Add(this.chkAutoScroll);
            this.pnlConsoleHeader.Controls.Add(this.btnCopyLog);
            this.pnlConsoleHeader.Controls.Add(this.btnClearLog);
            this.pnlConsoleHeader.Controls.Add(this.lblConsoleTitle);
            this.pnlConsoleHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConsoleHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlConsoleHeader.Name = "pnlConsoleHeader";
            this.pnlConsoleHeader.Size = new System.Drawing.Size(1080, 36);
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkAutoScroll.ForeColor = System.Drawing.Color.White;
            this.chkAutoScroll.Location = new System.Drawing.Point(801, 10);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(84, 17);
            this.chkAutoScroll.Text = "Auto-scroll";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // btnCopyLog
            // 
            this.btnCopyLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyLog.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
            this.btnCopyLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopyLog.ForeColor = System.Drawing.Color.White;
            this.btnCopyLog.Location = new System.Drawing.Point(894, 5);
            this.btnCopyLog.Name = "btnCopyLog";
            this.btnCopyLog.Size = new System.Drawing.Size(85, 26);
            this.btnCopyLog.Text = "📋 Copy Log";
            this.btnCopyLog.UseVisualStyleBackColor = false;
            this.btnCopyLog.Click += new System.EventHandler(this.btnCopyLog_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnClearLog.ForeColor = System.Drawing.Color.White;
            this.btnClearLog.Location = new System.Drawing.Point(985, 5);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(85, 26);
            this.btnClearLog.Text = "🧹 Clear";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lblConsoleTitle
            // 
            this.lblConsoleTitle.AutoSize = true;
            this.lblConsoleTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConsoleTitle.ForeColor = System.Drawing.Color.White;
            this.lblConsoleTitle.Location = new System.Drawing.Point(12, 10);
            this.lblConsoleTitle.Name = "lblConsoleTitle";
            this.lblConsoleTitle.Size = new System.Drawing.Size(155, 15);
            this.lblConsoleTitle.Text = "💻 Terminal / Runner Output";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1080, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlAddProject);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimumSize = new System.Drawing.Size(850, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kafka & Go Service Runner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlAddProject.ResumeLayout(false);
            this.pnlAddProject.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
            this.cmsProject.ResumeLayout(false);
            this.pnlTableToolbar.ResumeLayout(false);
            this.pnlConsoleHeader.ResumeLayout(false);
            this.pnlConsoleHeader.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
