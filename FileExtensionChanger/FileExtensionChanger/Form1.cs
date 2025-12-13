using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileExtensionChanger
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = string.Empty;
        private Dictionary<string, string> extensions;
        private Panel mainPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel dragDropPanel;
        private Label lblDragDrop;
        private Label lblFileInfo;
        private Label lblCurrentExt;
        private TextBox txtCurrentExt;
        private Label lblNewExt;
        private ComboBox cmbExtensions;
        private TextBox txtCustomExt;
        private Button btnBrowse;
        private Button btnRename;
        private Button btnReset;
        private FlowLayoutPanel flowExtensions;
        private Label lblPopular;
        private Panel separator1;
        private Panel separator2;

        public Form1()
        {
            InitializeComponent();
            SetupDragDrop();
            InitializeExtensions();
            ApplyModernStyle();
        }

        private void InitializeExtensions()
        {
            extensions = new Dictionary<string, string>
            {

                { ".txt", "Text File" },
                { ".doc", "Word Document" },
                { ".docx", "Word Document (new)" },
                { ".pdf", "PDF Document" },
                { ".rtf", "Rich Text Format" },
                { ".odt", "OpenDocument Text" },
                

                { ".xls", "Excel Spreadsheet" },
                { ".xlsx", "Excel Spreadsheet (new)" },
                { ".csv", "CSV Data" },
                { ".ods", "OpenDocument Spreadsheet" },
                

                { ".ppt", "PowerPoint Presentation" },
                { ".pptx", "PowerPoint Presentation (new)" },
                { ".odp", "OpenDocument Presentation" },
                

                { ".jpg", "JPEG Image" },
                { ".jpeg", "JPEG Image" },
                { ".png", "PNG Image" },
                { ".gif", "Animated Image" },
                { ".bmp", "Bitmap Image" },
                { ".svg", "Vector Image" },
                { ".ico", "Icon" },
                { ".webp", "WebP Image" },
                

                { ".zip", "ZIP Archive" },
                { ".rar", "RAR Archive" },
                { ".7z", "7-Zip Archive" },
                { ".tar", "TAR Archive" },
                { ".gz", "GZIP Archive" },
                

                { ".mp3", "MP3 Audio" },
                { ".wav", "WAV Audio" },
                { ".flac", "FLAC Audio" },
                { ".ogg", "OGG Audio" },
                { ".m4a", "AAC Audio" },
                

                { ".mp4", "MP4 Video" },
                { ".avi", "AVI Video" },
                { ".mkv", "Matroska Video" },
                { ".mov", "QuickTime Video" },
                { ".wmv", "Windows Media Video" },
                { ".flv", "Flash Video" },
                

                { ".html", "HTML Document" },
                { ".htm", "HTML Document" },
                { ".css", "Stylesheet" },
                { ".js", "JavaScript File" },
                { ".json", "JSON Data" },
                { ".xml", "XML Document" },
                

                { ".cs", "C# Code" },
                { ".cpp", "C++ Code" },
                { ".java", "Java Code" },
                { ".py", "Python Script" },
                { ".php", "PHP Script" },
                

                { ".exe", "Windows Executable" },
                { ".msi", "Windows Installer" },
                { ".bat", "Batch File" },
                { ".sh", "Shell Script" },
                

                { ".ini", "Configuration File" },
                { ".log", "Log File" },
                { ".bak", "Backup File" },
                { ".tmp", "Temporary File" }
            };


            cmbExtensions.Items.Clear();
            foreach (var ext in extensions.OrderBy(e => e.Value))
            {
                cmbExtensions.Items.Add($"{ext.Key} - {ext.Value}");
            }
            cmbExtensions.SelectedIndex = 0;


            string[] popularExtensions = { ".txt", ".docx", ".pdf", ".jpg", ".png", ".mp3", ".mp4", ".zip", ".rar" };
            foreach (var ext in popularExtensions)
            {
                if (extensions.ContainsKey(ext))
                {
                    var btn = new Button
                    {
                        Text = ext,
                        Tag = ext,
                        Size = new Size(60, 25),
                        Margin = new Padding(3),
                        BackColor = Color.FromArgb(230, 240, 255),
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += PopularExtension_Click;
                    flowExtensions.Controls.Add(btn);
                }
            }
        }

        private void PopularExtension_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                cmbExtensions.SelectedItem = cmbExtensions.Items
                    .Cast<string>()
                    .FirstOrDefault(item => item.StartsWith(btn.Tag.ToString()));
            }
        }

        private void InitializeComponent()
        {
            this.mainPanel = new Panel();
            this.separator2 = new Panel();
            this.separator1 = new Panel();
            this.lblPopular = new Label();
            this.flowExtensions = new FlowLayoutPanel();
            this.btnReset = new Button();
            this.btnRename = new Button();
            this.btnBrowse = new Button();
            this.txtCustomExt = new TextBox();
            this.cmbExtensions = new ComboBox();
            this.lblNewExt = new Label();
            this.txtCurrentExt = new TextBox();
            this.lblCurrentExt = new Label();
            this.lblFileInfo = new Label();
            this.dragDropPanel = new Panel();
            this.lblDragDrop = new Label();
            this.lblSubtitle = new Label();
            this.lblTitle = new Label();
            this.mainPanel.SuspendLayout();
            this.dragDropPanel.SuspendLayout();
            this.SuspendLayout();


            this.mainPanel.BackColor = Color.White;
            this.mainPanel.Controls.Add(this.separator2);
            this.mainPanel.Controls.Add(this.separator1);
            this.mainPanel.Controls.Add(this.lblPopular);
            this.mainPanel.Controls.Add(this.flowExtensions);
            this.mainPanel.Controls.Add(this.btnReset);
            this.mainPanel.Controls.Add(this.btnRename);
            this.mainPanel.Controls.Add(this.btnBrowse);
            this.mainPanel.Controls.Add(this.txtCustomExt);
            this.mainPanel.Controls.Add(this.cmbExtensions);
            this.mainPanel.Controls.Add(this.lblNewExt);
            this.mainPanel.Controls.Add(this.txtCurrentExt);
            this.mainPanel.Controls.Add(this.lblCurrentExt);
            this.mainPanel.Controls.Add(this.lblFileInfo);
            this.mainPanel.Controls.Add(this.dragDropPanel);
            this.mainPanel.Controls.Add(this.lblSubtitle);
            this.mainPanel.Controls.Add(this.lblTitle);
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new Size(650, 650);
            this.mainPanel.TabIndex = 0;


            this.separator1.BackColor = Color.FromArgb(240, 240, 240);
            this.separator1.Location = new Point(30, 230);
            this.separator1.Name = "separator1";
            this.separator1.Size = new Size(590, 1);
            this.separator1.TabIndex = 17;


            this.separator2.BackColor = Color.FromArgb(240, 240, 240);
            this.separator2.Location = new Point(30, 360);
            this.separator2.Name = "separator2";
            this.separator2.Size = new Size(590, 1);
            this.separator2.TabIndex = 18;


            this.lblPopular.AutoSize = true;
            this.lblPopular.Location = new Point(30, 370);
            this.lblPopular.Name = "lblPopular";
            this.lblPopular.Size = new Size(124, 20);
            this.lblPopular.TabIndex = 16;
            this.lblPopular.Text = "Popular extensions:";


            this.flowExtensions.Location = new Point(30, 395);
            this.flowExtensions.Name = "flowExtensions";
            this.flowExtensions.Size = new Size(590, 40);
            this.flowExtensions.TabIndex = 15;


            this.btnReset.Location = new Point(430, 580);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new Size(190, 45);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new EventHandler(btnReset_Click);


            this.btnRename.Location = new Point(230, 580);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new Size(190, 45);
            this.btnRename.TabIndex = 13;
            this.btnRename.Text = "Change Extension";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new EventHandler(btnRename_Click);


            this.btnBrowse.Location = new Point(30, 580);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new Size(190, 45);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Select File...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new EventHandler(btnBrowse_Click);


            this.txtCustomExt.Location = new Point(30, 520);
            this.txtCustomExt.Name = "txtCustomExt";
            this.txtCustomExt.PlaceholderText = "Or enter custom extension...";
            this.txtCustomExt.Size = new Size(590, 27);
            this.txtCustomExt.TabIndex = 11;
            this.txtCustomExt.KeyPress += new KeyPressEventHandler(txtCustomExt_KeyPress);


            this.cmbExtensions.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbExtensions.FormattingEnabled = true;
            this.cmbExtensions.Location = new Point(30, 480);
            this.cmbExtensions.Name = "cmbExtensions";
            this.cmbExtensions.Size = new Size(590, 28);
            this.cmbExtensions.TabIndex = 10;
            this.cmbExtensions.SelectedIndexChanged += new EventHandler(cmbExtensions_SelectedIndexChanged);


            this.lblNewExt.AutoSize = true;
            this.lblNewExt.Location = new Point(30, 455);
            this.lblNewExt.Name = "lblNewExt";
            this.lblNewExt.Size = new Size(166, 20);
            this.lblNewExt.TabIndex = 9;
            this.lblNewExt.Text = "Select new extension:";


            this.txtCurrentExt.Location = new Point(30, 310);
            this.txtCurrentExt.Name = "txtCurrentExt";
            this.txtCurrentExt.ReadOnly = true;
            this.txtCurrentExt.Size = new Size(590, 27);
            this.txtCurrentExt.TabIndex = 8;


            this.lblCurrentExt.AutoSize = true;
            this.lblCurrentExt.Location = new Point(30, 285);
            this.lblCurrentExt.Name = "lblCurrentExt";
            this.lblCurrentExt.Size = new Size(144, 20);
            this.lblCurrentExt.TabIndex = 7;
            this.lblCurrentExt.Text = "Current extension:";


            this.lblFileInfo.Location = new Point(30, 240);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new Size(590, 40);
            this.lblFileInfo.TabIndex = 6;
            this.lblFileInfo.Text = "No file selected";
            this.lblFileInfo.TextAlign = ContentAlignment.MiddleLeft;


            this.dragDropPanel.BackColor = Color.FromArgb(248, 250, 252);
            this.dragDropPanel.BorderStyle = BorderStyle.FixedSingle;
            this.dragDropPanel.Controls.Add(this.lblDragDrop);
            this.dragDropPanel.Cursor = Cursors.Hand;
            this.dragDropPanel.Location = new Point(30, 100);
            this.dragDropPanel.Name = "dragDropPanel";
            this.dragDropPanel.Size = new Size(590, 120);
            this.dragDropPanel.TabIndex = 5;


            this.lblDragDrop.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            this.lblDragDrop.Location = new Point(10, 10);
            this.lblDragDrop.Name = "lblDragDrop";
            this.lblDragDrop.Size = new Size(570, 100);
            this.lblDragDrop.TabIndex = 0;
            this.lblDragDrop.Text = "Drag and drop file here\r\nor\r\nclick to select file";
            this.lblDragDrop.TextAlign = ContentAlignment.MiddleCenter;


            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Location = new Point(30, 65);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new Size(345, 20);
            this.lblSubtitle.TabIndex = 4;
            this.lblSubtitle.Text = "Change file extension quickly and easily";


            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            this.lblTitle.Location = new Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(291, 41);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "File Extension Changer";


            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(650, 650);
            this.Controls.Add(mainPanel);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "File Extension Changer";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.dragDropPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void ApplyModernStyle()
        {

            Font = new Font("Segoe UI", 9F);


            lblTitle.ForeColor = Color.FromArgb(45, 45, 45);


            lblSubtitle.ForeColor = Color.FromArgb(100, 100, 100);


            dragDropPanel.BackColor = Color.FromArgb(248, 250, 252);
            dragDropPanel.BorderStyle = BorderStyle.FixedSingle;


            lblFileInfo.Font = new Font("Segoe UI", 9F);
            lblFileInfo.ForeColor = Color.FromArgb(80, 80, 80);


            lblCurrentExt.ForeColor = Color.FromArgb(60, 60, 60);
            lblNewExt.ForeColor = Color.FromArgb(60, 60, 60);
            lblPopular.ForeColor = Color.FromArgb(60, 60, 60);


            txtCurrentExt.BackColor = Color.FromArgb(250, 250, 250);
            txtCurrentExt.BorderStyle = BorderStyle.FixedSingle;
            txtCurrentExt.ForeColor = Color.FromArgb(50, 50, 50);

            cmbExtensions.BackColor = Color.White;
            cmbExtensions.FlatStyle = FlatStyle.Flat;

            txtCustomExt.BackColor = Color.FromArgb(250, 250, 250);
            txtCustomExt.BorderStyle = BorderStyle.FixedSingle;


            btnBrowse.BackColor = Color.FromArgb(52, 152, 219);
            btnBrowse.ForeColor = Color.White;
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.FlatAppearance.BorderSize = 0;
            btnBrowse.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBrowse.Cursor = Cursors.Hand;

            btnRename.BackColor = Color.FromArgb(46, 204, 113);
            btnRename.ForeColor = Color.White;
            btnRename.FlatStyle = FlatStyle.Flat;
            btnRename.FlatAppearance.BorderSize = 0;
            btnRename.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRename.Cursor = Cursors.Hand;

            btnReset.BackColor = Color.FromArgb(231, 76, 60);
            btnReset.ForeColor = Color.White;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReset.Cursor = Cursors.Hand;


            foreach (Control control in flowExtensions.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = Color.FromArgb(230, 240, 255);
                    btn.ForeColor = Color.FromArgb(41, 128, 185);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(200, 220, 240);
                    btn.FlatAppearance.BorderSize = 1;
                    btn.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
                    btn.Cursor = Cursors.Hand;
                }
            }

            separator1.BackColor = Color.FromArgb(240, 240, 240);
            separator2.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void SetupDragDrop()
        {
            this.AllowDrop = true;
            dragDropPanel.AllowDrop = true;

            dragDropPanel.DragEnter += new DragEventHandler(DragDropPanel_DragEnter);
            dragDropPanel.DragDrop += new DragEventHandler(DragDropPanel_DragDrop);
            dragDropPanel.Click += new EventHandler(DragDropPanel_Click);

            lblDragDrop.Click += new EventHandler(DragDropPanel_Click);
        }

        private void DragDropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                dragDropPanel.BackColor = Color.FromArgb(235, 245, 255);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DragDropPanel_DragLeave(object sender, EventArgs e)
        {
            dragDropPanel.BackColor = Color.FromArgb(248, 250, 252);
        }

        private void DragDropPanel_DragDrop(object sender, DragEventArgs e)
        {
            dragDropPanel.BackColor = Color.FromArgb(248, 250, 252);
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                ProcessSelectedFile(files[0]);
            }
        }

        private void DragDropPanel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select File";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ProcessSelectedFile(openFileDialog.FileName);
                }
            }
        }

        private void ProcessSelectedFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    selectedFilePath = filePath;
                    string extension = Path.GetExtension(filePath);
                    string fileName = Path.GetFileName(filePath);
                    string fileSize = GetFileSize(new FileInfo(filePath).Length);

                    txtCurrentExt.Text = extension;
                    lblFileInfo.Text = $"File: {fileName}\nPath: {Path.GetDirectoryName(filePath)}\nSize: {fileSize}";


                    var matchingItem = cmbExtensions.Items
                        .Cast<string>()
                        .FirstOrDefault(item => item.StartsWith(extension));

                    if (matchingItem != null)
                    {
                        cmbExtensions.SelectedItem = matchingItem;
                    }
                    else
                    {
                        cmbExtensions.SelectedIndex = 0;
                    }

                    txtCustomExt.Clear();
                    cmbExtensions.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select File";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ProcessSelectedFile(openFileDialog.FileName);
                }
            }
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file first!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newExtension;

            if (!string.IsNullOrEmpty(txtCustomExt.Text))
            {

                newExtension = txtCustomExt.Text.Trim();
            }
            else if (cmbExtensions.SelectedItem != null)
            {

                string selected = cmbExtensions.SelectedItem.ToString();
                newExtension = selected.Substring(0, selected.IndexOf(' '));
            }
            else
            {
                MessageBox.Show("Please select a new extension or enter custom one!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!newExtension.StartsWith("."))
                {
                    newExtension = "." + newExtension;
                }

                string directory = Path.GetDirectoryName(selectedFilePath);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(selectedFilePath);
                string newFilePath = Path.Combine(directory, fileNameWithoutExt + newExtension);

                if (File.Exists(newFilePath))
                {
                    DialogResult result = MessageBox.Show(
                        $"File '{Path.GetFileName(newFilePath)}' already exists.\nOverwrite it?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                File.Move(selectedFilePath, newFilePath);
                selectedFilePath = newFilePath;

                txtCurrentExt.Text = newExtension;
                lblFileInfo.Text = $"File successfully changed!\n{Path.GetFileName(newFilePath)}";
                txtCustomExt.Clear();

                MessageBox.Show($"File successfully renamed!\nNew extension: {newExtension}",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming file: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            selectedFilePath = string.Empty;
            txtCurrentExt.Clear();
            txtCustomExt.Clear();
            lblFileInfo.Text = "No file selected";
            cmbExtensions.SelectedIndex = 0;
        }

        private void txtCustomExt_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCustomExt.Text))
            {
                txtCustomExt.Clear();
            }
        }
    }
}
