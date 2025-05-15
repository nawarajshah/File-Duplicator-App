namespace File_Duplicator_App
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtFolderPath = new TextBox();
            txtFind = new TextBox();
            txtDuplicate = new TextBox();
            btnBrowse = new Button();
            btnSubmit = new Button();
            clbFolders = new CheckedListBox();
            btnSaveConfig = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label1.Location = new Point(176, 25);
            label1.Name = "label1";
            label1.Size = new Size(292, 41);
            label1.TabIndex = 0;
            label1.Text = "File Duplicator App";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 114);
            label2.Name = "label2";
            label2.Size = new Size(105, 25);
            label2.TabIndex = 1;
            label2.Text = "Folder Path:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 163);
            label3.Name = "label3";
            label3.Size = new Size(50, 25);
            label3.TabIndex = 2;
            label3.Text = "Find:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 217);
            label4.Name = "label4";
            label4.Size = new Size(90, 25);
            label4.TabIndex = 3;
            label4.Text = "Duplicate:";
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(176, 108);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(292, 31);
            txtFolderPath.TabIndex = 4;
            // 
            // txtFind
            // 
            txtFind.Location = new Point(176, 157);
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(292, 31);
            txtFind.TabIndex = 5;
            // 
            // txtDuplicate
            // 
            txtDuplicate.Location = new Point(176, 211);
            txtDuplicate.Name = "txtDuplicate";
            txtDuplicate.Size = new Size(292, 31);
            txtDuplicate.TabIndex = 6;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(474, 106);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(112, 34);
            btnBrowse.TabIndex = 7;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(176, 294);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(112, 34);
            btnSubmit.TabIndex = 8;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // clbFolders
            // 
            clbFolders.FormattingEnabled = true;
            clbFolders.Location = new Point(641, 25);
            clbFolders.Name = "clbFolders";
            clbFolders.Size = new Size(202, 256);
            clbFolders.TabIndex = 9;
            clbFolders.MouseClick += clbFolders_MouseClick;
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(641, 294);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(202, 34);
            btnSaveConfig.TabIndex = 10;
            btnSaveConfig.Text = "Save Config";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += btnSaveConfig_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(891, 378);
            Controls.Add(btnSaveConfig);
            Controls.Add(clbFolders);
            Controls.Add(btnSubmit);
            Controls.Add(btnBrowse);
            Controls.Add(txtDuplicate);
            Controls.Add(txtFind);
            Controls.Add(txtFolderPath);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox txtFolderPath;
        private TextBox txtFind;
        private TextBox txtDuplicate;
        private Button btnBrowse;
        private Button btnSubmit;
        private CheckedListBox clbFolders;
        private Button btnSaveConfig;
    }
}
