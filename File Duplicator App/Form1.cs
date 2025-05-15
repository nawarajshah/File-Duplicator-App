using System.Text.RegularExpressions;

namespace File_Duplicator_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = folderDialog.SelectedPath;
                    LoadFolderList(folderDialog.SelectedPath);
                }
            }
        }

        private void LoadFolderList(string folderPath)
        {
            string configFilePath = Path.Combine(folderPath, "file");
            var selectedFolders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (File.Exists(configFilePath))
            {
                selectedFolders = new HashSet<string>(File.ReadAllLines(configFilePath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Trim()),
                    StringComparer.OrdinalIgnoreCase);
            }

            var folders = Directory.GetDirectories(folderPath)
                .Select(d => Path.GetFileName(d))
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .ToList();

            clbFolders.Items.Clear();

            foreach (var name in folders)
            {
                clbFolders.Items.Add(name, selectedFolders.Contains(name));
            }

            SortCheckedListBox();
        }

        private void SortCheckedListBox()
        {
            var checkedItems = clbFolders.CheckedItems.Cast<string>().OrderBy(x => x).ToList();
            var uncheckedItems = clbFolders.Items.Cast<string>().Except(checkedItems).OrderBy(x => x).ToList();

            clbFolders.Items.Clear();

            foreach (var item in checkedItems)
                clbFolders.Items.Add(item, true);

            foreach (var item in uncheckedItems)
                clbFolders.Items.Add(item, false);
        }

        private void clbFolders_MouseClick(object sender, MouseEventArgs e)
        {
            int index = clbFolders.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                clbFolders.SetItemChecked(index, !clbFolders.GetItemChecked(index));
                SortCheckedListBox();
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text.Trim();
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("Please select a folder path first.");
                return;
            }

            string configFilePath = Path.Combine(folderPath, "file");
            var selectedFolders = clbFolders.CheckedItems.Cast<string>();
            File.WriteAllLines(configFilePath, selectedFolders);
            MessageBox.Show("Configuration saved successfully.");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string folderPath = txtFolderPath.Text.Trim();
            string findName = txtFind.Text.Trim();
            string duplicateName = txtDuplicate.Text.Trim();

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(findName) || string.IsNullOrEmpty(duplicateName))
            {
                MessageBox.Show("Please provide Folder Path, Find, and Duplicate names.");
                return;
            }

            string configFilePath = Path.Combine(folderPath, "file");
            if (!File.Exists(configFilePath))
            {
                MessageBox.Show("Configuration file not found.");
                return;
            }

            string logFilePath = Path.Combine(folderPath, "duplication.log");

            try
            {
                var duplicator = new FileDuplicatorService(folderPath);
                duplicator.DuplicateFiles(folderPath, findName, duplicateName);

                var result = MessageBox.Show(
                    $"Duplication completed. For details, see the log file:\n{logFilePath}\n\nDo you want to open the log file now?",
                    "Duplication Completed",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = logFilePath,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Could not open log file: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Duplication failed: {ex.Message}\nSee the log file for details:\n{logFilePath}");
            }
        }

    }

    public class FileDuplicatorService
    {
        private readonly string logFilePath;

        public FileDuplicatorService(string folderPath)
        {
            logFilePath = Path.Combine(folderPath, "duplication.log");
        }

        private void Log(string message)
        {
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

        public void DuplicateFiles(string folderPath, string find, string duplicate)
        {
            var folders = Directory.GetDirectories(folderPath);

            foreach (var folder in folders)
            {
                var files = Directory.GetFiles(folder)
                    .Where(f => Path.GetFileNameWithoutExtension(f)
                    .Contains(find, StringComparison.OrdinalIgnoreCase));

                foreach (var file in files)
                {
                    var ext = Path.GetExtension(file);
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    var newFileName = fileName.Replace(find, duplicate, StringComparison.OrdinalIgnoreCase);
                    var newFilePath = Path.Combine(folder, newFileName + ext);

                    try
                    {
                        var content = File.ReadAllText(file);
                        content = Regex.Replace(content, Regex.Escape(find), duplicate, RegexOptions.IgnoreCase);

                        File.WriteAllText(newFilePath, content);

                        Log($"SUCCESS: Duplicated '{file}' to '{newFilePath}'.");
                    }
                    catch (Exception ex)
                    {
                        Log($"ERROR: Failed to duplicate '{file}' to '{newFilePath}'. Exception: {ex.Message}");
                    }
                }
            }
        }
    }

}
