namespace CKANSyncApplications
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hideBtn = new System.Windows.Forms.Button();
            this.msgBox = new System.Windows.Forms.TextBox();
            this.settingBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fileWatcher = new System.IO.FileSystemWatcher();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.syncPathName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CKAN1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // hideBtn
            // 
            this.hideBtn.Location = new System.Drawing.Point(472, 42);
            this.hideBtn.Name = "hideBtn";
            this.hideBtn.Size = new System.Drawing.Size(75, 23);
            this.hideBtn.TabIndex = 0;
            this.hideBtn.Text = "Hide";
            this.hideBtn.UseVisualStyleBackColor = true;
            // 
            // msgBox
            // 
            this.msgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.msgBox.ForeColor = System.Drawing.Color.Red;
            this.msgBox.Location = new System.Drawing.Point(12, 42);
            this.msgBox.Multiline = true;
            this.msgBox.Name = "msgBox";
            this.msgBox.ShortcutsEnabled = false;
            this.msgBox.Size = new System.Drawing.Size(439, 101);
            this.msgBox.TabIndex = 2;
            this.msgBox.Text = "This program has connected with the CKAN platform";
            this.msgBox.TextChanged += new System.EventHandler(this.msgBox_TextChanged);
            // 
            // settingBtn
            // 
            this.settingBtn.Location = new System.Drawing.Point(472, 120);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(75, 23);
            this.settingBtn.TabIndex = 3;
            this.settingBtn.Text = "Settings";
            this.settingBtn.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(347, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 214);
            this.panel1.TabIndex = 4;
            // 
            // fileWatcher
            // 
            this.fileWatcher.EnableRaisingEvents = true;
            this.fileWatcher.SynchronizingObject = this;
            this.fileWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileWatcher_Changed);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CKAN1,
            this.FileName1});
            this.dataGridView1.Location = new System.Drawing.Point(12, 164);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(320, 214);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // syncPathName
            // 
            this.syncPathName.AutoSize = true;
            this.syncPathName.Location = new System.Drawing.Point(122, 24);
            this.syncPathName.Name = "syncPathName";
            this.syncPathName.Size = new System.Drawing.Size(110, 12);
            this.syncPathName.TabIndex = 7;
            this.syncPathName.Text = "C:\\Users\\Allen\\CKAN\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sync. Directory Path:";
            // 
            // CKAN1
            // 
            this.CKAN1.HeaderText = "Directory Name";
            this.CKAN1.Name = "CKAN1";
            this.CKAN1.ReadOnly = true;
            // 
            // FileName1
            // 
            this.FileName1.HeaderText = "File Name";
            this.FileName1.Name = "FileName1";
            this.FileName1.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LemonChiffon;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(577, 418);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.syncPathName);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.settingBtn);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.hideBtn);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Sync App  for CKAN";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button hideBtn;
        private System.Windows.Forms.TextBox msgBox;
        private System.Windows.Forms.Button settingBtn;
        private System.Windows.Forms.Panel panel1;
        private System.IO.FileSystemWatcher fileWatcher;
        private System.Windows.Forms.Label syncPathName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CKAN1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName1;
    }
}

