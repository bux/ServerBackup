namespace ServerBackup {
    partial class ServerBackup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnBackupDatabases = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBackupFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackupDatabases
            // 
            this.btnBackupDatabases.Location = new System.Drawing.Point(12, 150);
            this.btnBackupDatabases.Name = "btnBackupDatabases";
            this.btnBackupDatabases.Size = new System.Drawing.Size(119, 23);
            this.btnBackupDatabases.TabIndex = 0;
            this.btnBackupDatabases.Text = "Backup Databases";
            this.btnBackupDatabases.UseVisualStyleBackColor = true;
            this.btnBackupDatabases.Click += new System.EventHandler(this.btnBackupDatabases_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 132);
            this.textBox1.TabIndex = 1;
            // 
            // btnBackupFiles
            // 
            this.btnBackupFiles.Location = new System.Drawing.Point(12, 179);
            this.btnBackupFiles.Name = "btnBackupFiles";
            this.btnBackupFiles.Size = new System.Drawing.Size(119, 23);
            this.btnBackupFiles.TabIndex = 2;
            this.btnBackupFiles.Text = "Backup Files";
            this.btnBackupFiles.UseVisualStyleBackColor = true;
            this.btnBackupFiles.Click += new System.EventHandler(this.btnBackupFiles_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnBackupFiles);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnBackupDatabases);
            this.Name = "Form1";
            this.Text = "ServerBackup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBackupDatabases;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnBackupFiles;
    }
}

