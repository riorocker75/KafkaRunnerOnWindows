using System;
using System.IO;
using System.Windows.Forms;
using kafkaRunnerWindows.Models;

namespace kafkaRunnerWindows.Forms
{
    public partial class SettingsForm : Form
    {
        public RunnerConfig Config { get; private set; }

        public SettingsForm(RunnerConfig currentConfig)
        {
            InitializeComponent();
            try
            {
                var exePath = Environment.ProcessPath ?? Application.ExecutablePath;
                if (!string.IsNullOrEmpty(exePath) && File.Exists(exePath))
                {
                    var ico = System.Drawing.Icon.ExtractAssociatedIcon(exePath);
                    if (ico != null) this.Icon = ico;
                }
            }
            catch { }

            Config = new RunnerConfig
            {
                MsysBinPath = currentConfig.MsysBinPath,
                GccPath = currentConfig.GccPath,
                MakeCommand = currentConfig.MakeCommand,
                ShowEnvDiagnostics = currentConfig.ShowEnvDiagnostics
            };

            txtMsysBin.Text = Config.MsysBinPath;
            txtGcc.Text = Config.GccPath;
            txtCommand.Text = Config.MakeCommand;
            chkDiagnostics.Checked = Config.ShowEnvDiagnostics;
        }

        private void btnBrowseMsys_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.Description = "Select MSYS2 / MinGW Bin Directory";
            if (Directory.Exists(txtMsysBin.Text.Trim()))
            {
                dlg.SelectedPath = txtMsysBin.Text.Trim();
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtMsysBin.Text = dlg.SelectedPath;
                var autoGcc = Path.Combine(dlg.SelectedPath, "gcc.exe");
                if (File.Exists(autoGcc) && string.IsNullOrWhiteSpace(txtGcc.Text))
                {
                    txtGcc.Text = autoGcc;
                }
            }
        }

        private void btnBrowseGcc_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            dlg.Title = "Select GCC Executable";
            if (File.Exists(txtGcc.Text.Trim()))
            {
                dlg.InitialDirectory = Path.GetDirectoryName(txtGcc.Text.Trim());
                dlg.FileName = Path.GetFileName(txtGcc.Text.Trim());
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtGcc.Text = dlg.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Config.MsysBinPath = txtMsysBin.Text.Trim();
            Config.GccPath = txtGcc.Text.Trim();
            Config.MakeCommand = string.IsNullOrWhiteSpace(txtCommand.Text) ? "make run" : txtCommand.Text.Trim();
            Config.ShowEnvDiagnostics = chkDiagnostics.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
