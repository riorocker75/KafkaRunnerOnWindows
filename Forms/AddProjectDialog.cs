using System;
using System.IO;
using System.Windows.Forms;
using kafkaRunnerWindows.Models;

namespace kafkaRunnerWindows.Forms
{
    public partial class AddProjectDialog : Form
    {
        public ProjectRecord Project { get; private set; }
        private readonly bool _isEditMode;

        public AddProjectDialog(string initialPath = "", ProjectRecord? existing = null)
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

            _isEditMode = existing != null;

            if (_isEditMode && existing != null)
            {
                Text = "Edit Project Task & Feature";
                lblHeader.Text = "Edit Project & Task Information";
                btnSave.Text = "Save Changes";

                Project = existing;
                txtPath.Text = existing.Path;
                txtName.Text = existing.Name;
                txtFeatureTask.Text = existing.FeatureTask;
                cboWorkStatus.SelectedItem = existing.WorkStatus;
                if (cboWorkStatus.SelectedIndex < 0)
                {
                    cboWorkStatus.Text = existing.WorkStatus;
                }
            }
            else
            {
                Text = "Add Project";
                lblHeader.Text = "New Project & Task Information";
                btnSave.Text = "Add Project";

                Project = new ProjectRecord();
                if (!string.IsNullOrWhiteSpace(initialPath))
                {
                    txtPath.Text = initialPath.Trim();
                }
                cboWorkStatus.SelectedIndex = 0; // Default: In Progress
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.Description = "Select Go / Kafka Project Root Directory";
            dlg.UseDescriptionForTitle = true;

            if (!string.IsNullOrWhiteSpace(txtPath.Text) && Directory.Exists(txtPath.Text.Trim()))
            {
                dlg.SelectedPath = txtPath.Text.Trim();
            }

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                txtPath.Text = dlg.SelectedPath;
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            var path = txtPath.Text.Trim();
            if (!_isEditMode || string.IsNullOrWhiteSpace(txtName.Text))
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var folder = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                    if (!string.IsNullOrWhiteSpace(folder))
                    {
                        txtName.Text = folder;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var raw = txtPath.Text?.Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                MessageBox.Show("Please enter or select a project directory folder.", "Add Project", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPath.Focus();
                return;
            }

            try
            {
                var expanded = Environment.ExpandEnvironmentVariables(raw);
                var full = Path.GetFullPath(expanded);

                var name = txtName.Text?.Trim();
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = Path.GetFileName(full.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                    if (string.IsNullOrWhiteSpace(name)) name = full;
                }

                Project.Path = full;
                Project.Name = name;
                Project.FeatureTask = txtFeatureTask.Text?.Trim() ?? string.Empty;
                Project.WorkStatus = string.IsNullOrWhiteSpace(cboWorkStatus.Text) ? "In Progress" : cboWorkStatus.Text.Trim();

                if (!_isEditMode)
                {
                    Project.AddedAt = DateTime.UtcNow;
                    Project.Status = Directory.Exists(full) ? "Ready" : "Folder Missing";
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Invalid directory path:\n{ex.Message}", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
