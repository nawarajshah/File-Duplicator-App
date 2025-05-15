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

            var selectedFolders = File.ReadAllLines(configFilePath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            foreach (var folderName in selectedFolders)
            {
                string fullFolderPath = Path.Combine(folderPath, folderName);

                if (Directory.Exists(fullFolderPath))
                {
                    var files = Directory.GetFiles(fullFolderPath);

                    foreach (var filePath in files)
                    {
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                        string extension = Path.GetExtension(filePath);

                        if (fileNameWithoutExtension.Contains(findName, StringComparison.OrdinalIgnoreCase))
                        {
                            string[] parts = fileNameWithoutExtension.Split('_');
                            for (int i = 0; i < parts.Length; i++)
                            {
                                if (string.Equals(parts[i], findName, StringComparison.OrdinalIgnoreCase))
                                    parts[i] = duplicateName;
                            }

                            string newFileNameWithOutExtension = string.Join("_", parts);
                            string newFilePath = Path.Combine(fullFolderPath, newFileNameWithOutExtension + extension);

                            try
                            {
                                File.Copy(filePath, newFilePath, overwrite: false);

                                string content = File.ReadAllText(newFilePath);
                                content = ReplaceInsensitive(content, findName, duplicateName);
                                File.WriteAllText(newFilePath, content);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Failed to duplicate {filePath}: {ex.Message}");
                                continue;
                            }
                        }
                    }
                }
            }

            MessageBox.Show("Duplication Completed Successfully!");
        }

        private string ReplaceInsensitive(string input, string find, string replace)
        {
            return Regex.Replace(input, Regex.Escape(find), replace, RegexOptions.IgnoreCase);
        }
    }
}
