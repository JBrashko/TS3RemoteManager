namespace ClientQueryMonitor
{
    partial class RemoteManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConnectButton = new System.Windows.Forms.Button();
            this.hostStart = new System.Windows.Forms.Button();
            this.sndButton = new System.Windows.Forms.Button();
            this.sndBox = new System.Windows.Forms.TextBox();
            this.MessageTabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.MessageView = new System.Windows.Forms.ListView();
            this.messageHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.recievedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LogTab = new System.Windows.Forms.TabPage();
            this.LogView = new System.Windows.Forms.ListView();
            this.logHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.managerPage = new System.Windows.Forms.TabPage();
            this.managerCQMessages = new System.Windows.Forms.ListView();
            this.messageHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RecievedHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NotifyMessages = new System.Windows.Forms.TabPage();
            this.NotifyMessageView = new System.Windows.Forms.ListView();
            this.NotifyMessageHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NotifyRecievedHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NotifyTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AutomaticMessageTab = new System.Windows.Forms.TabPage();
            this.AutoMessageView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.hostSecureStart = new System.Windows.Forms.Button();
            this.hostUSBstart = new System.Windows.Forms.Button();
            this.USBScan = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MessageTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.LogTab.SuspendLayout();
            this.managerPage.SuspendLayout();
            this.NotifyMessages.SuspendLayout();
            this.AutomaticMessageTab.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(12, 27);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // hostStart
            // 
            this.hostStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostStart.Location = new System.Drawing.Point(567, 27);
            this.hostStart.Name = "hostStart";
            this.hostStart.Size = new System.Drawing.Size(75, 23);
            this.hostStart.TabIndex = 3;
            this.hostStart.Text = "Start host";
            this.hostStart.UseVisualStyleBackColor = true;
            this.hostStart.Click += new System.EventHandler(this.hostStart_Click);
            // 
            // sndButton
            // 
            this.sndButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sndButton.Location = new System.Drawing.Point(541, 258);
            this.sndButton.Name = "sndButton";
            this.sndButton.Size = new System.Drawing.Size(75, 23);
            this.sndButton.TabIndex = 5;
            this.sndButton.Text = "Send";
            this.sndButton.UseVisualStyleBackColor = true;
            this.sndButton.Click += new System.EventHandler(this.sndButton_Click);
            // 
            // sndBox
            // 
            this.sndBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sndBox.Location = new System.Drawing.Point(6, 261);
            this.sndBox.Name = "sndBox";
            this.sndBox.Size = new System.Drawing.Size(529, 20);
            this.sndBox.TabIndex = 6;
            this.sndBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.sndBox_KeyPress);
            // 
            // MessageTabControl
            // 
            this.MessageTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageTabControl.Controls.Add(this.tabPage2);
            this.MessageTabControl.Controls.Add(this.LogTab);
            this.MessageTabControl.Controls.Add(this.managerPage);
            this.MessageTabControl.Controls.Add(this.NotifyMessages);
            this.MessageTabControl.Controls.Add(this.AutomaticMessageTab);
            this.MessageTabControl.Location = new System.Drawing.Point(12, 56);
            this.MessageTabControl.Name = "MessageTabControl";
            this.MessageTabControl.SelectedIndex = 0;
            this.MessageTabControl.Size = new System.Drawing.Size(630, 405);
            this.MessageTabControl.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.MessageView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 379);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CQMessages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // MessageView
            // 
            this.MessageView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageHeader,
            this.recievedHeader,
            this.timeHeader});
            this.MessageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageView.GridLines = true;
            this.MessageView.Location = new System.Drawing.Point(3, 3);
            this.MessageView.Name = "MessageView";
            this.MessageView.Size = new System.Drawing.Size(616, 373);
            this.MessageView.TabIndex = 0;
            this.MessageView.UseCompatibleStateImageBehavior = false;
            this.MessageView.View = System.Windows.Forms.View.Details;
            // 
            // messageHeader
            // 
            this.messageHeader.Text = "Message";
            this.messageHeader.Width = 276;
            // 
            // recievedHeader
            // 
            this.recievedHeader.Text = "Recieved";
            this.recievedHeader.Width = 62;
            // 
            // timeHeader
            // 
            this.timeHeader.Text = "Time";
            // 
            // LogTab
            // 
            this.LogTab.Controls.Add(this.LogView);
            this.LogTab.Location = new System.Drawing.Point(4, 22);
            this.LogTab.Name = "LogTab";
            this.LogTab.Padding = new System.Windows.Forms.Padding(3);
            this.LogTab.Size = new System.Drawing.Size(622, 310);
            this.LogTab.TabIndex = 2;
            this.LogTab.Text = "RemoteLog";
            this.LogTab.UseVisualStyleBackColor = true;
            // 
            // LogView
            // 
            this.LogView.AutoArrange = false;
            this.LogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.logHeader,
            this.logTimeHeader});
            this.LogView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogView.GridLines = true;
            this.LogView.Location = new System.Drawing.Point(3, 3);
            this.LogView.Name = "LogView";
            this.LogView.Size = new System.Drawing.Size(616, 304);
            this.LogView.TabIndex = 1;
            this.LogView.UseCompatibleStateImageBehavior = false;
            this.LogView.View = System.Windows.Forms.View.Details;
            // 
            // logHeader
            // 
            this.logHeader.Text = "Log entry";
            this.logHeader.Width = 250;
            // 
            // logTimeHeader
            // 
            this.logTimeHeader.Text = "Time";
            // 
            // managerPage
            // 
            this.managerPage.Controls.Add(this.managerCQMessages);
            this.managerPage.Controls.Add(this.sndButton);
            this.managerPage.Controls.Add(this.sndBox);
            this.managerPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.managerPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.managerPage.Location = new System.Drawing.Point(4, 22);
            this.managerPage.Name = "managerPage";
            this.managerPage.Padding = new System.Windows.Forms.Padding(3);
            this.managerPage.Size = new System.Drawing.Size(622, 310);
            this.managerPage.TabIndex = 3;
            this.managerPage.Text = "Manager Messages";
            this.managerPage.UseVisualStyleBackColor = true;
            // 
            // managerCQMessages
            // 
            this.managerCQMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.managerCQMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.messageHeader1,
            this.RecievedHeader1,
            this.TimeHeader1});
            this.managerCQMessages.GridLines = true;
            this.managerCQMessages.Location = new System.Drawing.Point(3, 3);
            this.managerCQMessages.Name = "managerCQMessages";
            this.managerCQMessages.Size = new System.Drawing.Size(619, 249);
            this.managerCQMessages.TabIndex = 1;
            this.managerCQMessages.UseCompatibleStateImageBehavior = false;
            this.managerCQMessages.View = System.Windows.Forms.View.Details;
            // 
            // messageHeader1
            // 
            this.messageHeader1.Text = "Message";
            this.messageHeader1.Width = 276;
            // 
            // RecievedHeader1
            // 
            this.RecievedHeader1.Text = "Recieved";
            this.RecievedHeader1.Width = 62;
            // 
            // TimeHeader1
            // 
            this.TimeHeader1.Text = "Time";
            // 
            // NotifyMessages
            // 
            this.NotifyMessages.Controls.Add(this.NotifyMessageView);
            this.NotifyMessages.Location = new System.Drawing.Point(4, 22);
            this.NotifyMessages.Name = "NotifyMessages";
            this.NotifyMessages.Padding = new System.Windows.Forms.Padding(3);
            this.NotifyMessages.Size = new System.Drawing.Size(622, 310);
            this.NotifyMessages.TabIndex = 4;
            this.NotifyMessages.Text = "NotifyMessages";
            this.NotifyMessages.UseVisualStyleBackColor = true;
            // 
            // NotifyMessageView
            // 
            this.NotifyMessageView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NotifyMessageHeader,
            this.NotifyRecievedHeader,
            this.NotifyTimeHeader});
            this.NotifyMessageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NotifyMessageView.GridLines = true;
            this.NotifyMessageView.Location = new System.Drawing.Point(3, 3);
            this.NotifyMessageView.Name = "NotifyMessageView";
            this.NotifyMessageView.Size = new System.Drawing.Size(616, 304);
            this.NotifyMessageView.TabIndex = 1;
            this.NotifyMessageView.UseCompatibleStateImageBehavior = false;
            this.NotifyMessageView.View = System.Windows.Forms.View.Details;
            // 
            // NotifyMessageHeader
            // 
            this.NotifyMessageHeader.Text = "Message";
            this.NotifyMessageHeader.Width = 276;
            // 
            // NotifyRecievedHeader
            // 
            this.NotifyRecievedHeader.Text = "Recieved";
            this.NotifyRecievedHeader.Width = 62;
            // 
            // NotifyTimeHeader
            // 
            this.NotifyTimeHeader.Text = "Time";
            // 
            // AutomaticMessageTab
            // 
            this.AutomaticMessageTab.Controls.Add(this.AutoMessageView);
            this.AutomaticMessageTab.Location = new System.Drawing.Point(4, 22);
            this.AutomaticMessageTab.Name = "AutomaticMessageTab";
            this.AutomaticMessageTab.Padding = new System.Windows.Forms.Padding(3);
            this.AutomaticMessageTab.Size = new System.Drawing.Size(622, 310);
            this.AutomaticMessageTab.TabIndex = 5;
            this.AutomaticMessageTab.Text = "Automatic Messages";
            this.AutomaticMessageTab.UseVisualStyleBackColor = true;
            // 
            // AutoMessageView
            // 
            this.AutoMessageView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.AutoMessageView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AutoMessageView.GridLines = true;
            this.AutoMessageView.Location = new System.Drawing.Point(3, 3);
            this.AutoMessageView.Name = "AutoMessageView";
            this.AutoMessageView.Size = new System.Drawing.Size(616, 304);
            this.AutoMessageView.TabIndex = 2;
            this.AutoMessageView.UseCompatibleStateImageBehavior = false;
            this.AutoMessageView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 276;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Recieved";
            this.columnHeader2.Width = 62;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            // 
            // hostSecureStart
            // 
            this.hostSecureStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostSecureStart.Location = new System.Drawing.Point(464, 27);
            this.hostSecureStart.Name = "hostSecureStart";
            this.hostSecureStart.Size = new System.Drawing.Size(97, 23);
            this.hostSecureStart.TabIndex = 8;
            this.hostSecureStart.Text = "Start secure host";
            this.hostSecureStart.UseVisualStyleBackColor = true;
            this.hostSecureStart.Click += new System.EventHandler(this.hostSecureStart_Click);
            // 
            // hostUSBstart
            // 
            this.hostUSBstart.Location = new System.Drawing.Point(373, 27);
            this.hostUSBstart.Name = "hostUSBstart";
            this.hostUSBstart.Size = new System.Drawing.Size(85, 23);
            this.hostUSBstart.TabIndex = 9;
            this.hostUSBstart.Text = "Start USB host";
            this.hostUSBstart.UseVisualStyleBackColor = true;
            this.hostUSBstart.Click += new System.EventHandler(this.hostUSBstart_Click);
            // 
            // USBScan
            // 
            this.USBScan.Location = new System.Drawing.Point(269, 27);
            this.USBScan.Name = "USBScan";
            this.USBScan.Size = new System.Drawing.Size(98, 23);
            this.USBScan.TabIndex = 10;
            this.USBScan.Text = "Scan for devices";
            this.USBScan.UseVisualStyleBackColor = true;
            this.USBScan.Click += new System.EventHandler(this.USBScan_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(654, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(105, 17);
            this.toolStripStatusLabel1.Text = "Clients connected:";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSettingsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editSettingsToolStripMenuItem
            // 
            this.editSettingsToolStripMenuItem.Name = "editSettingsToolStripMenuItem";
            this.editSettingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editSettingsToolStripMenuItem.Text = "Edit Settings";
            this.editSettingsToolStripMenuItem.Click += new System.EventHandler(this.editSettingsToolStripMenuItem_Click);
            // 
            // RemoteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 486);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.USBScan);
            this.Controls.Add(this.hostUSBstart);
            this.Controls.Add(this.hostSecureStart);
            this.Controls.Add(this.MessageTabControl);
            this.Controls.Add(this.hostStart);
            this.Controls.Add(this.ConnectButton);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RemoteManager";
            this.Text = "CQRemoteManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RemoteManager_FormClosed);
            this.MessageTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.LogTab.ResumeLayout(false);
            this.managerPage.ResumeLayout(false);
            this.managerPage.PerformLayout();
            this.NotifyMessages.ResumeLayout(false);
            this.AutomaticMessageTab.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button hostStart;
        private System.Windows.Forms.Button sndButton;
        private System.Windows.Forms.TextBox sndBox;
        private System.Windows.Forms.TabControl MessageTabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage LogTab;
        private System.Windows.Forms.ListView LogView;
        private System.Windows.Forms.ListView MessageView;
        private System.Windows.Forms.ColumnHeader messageHeader;
        private System.Windows.Forms.ColumnHeader recievedHeader;
        private System.Windows.Forms.ColumnHeader logHeader;
        private System.Windows.Forms.TabPage managerPage;
        private System.Windows.Forms.ListView managerCQMessages;
        private System.Windows.Forms.ColumnHeader messageHeader1;
        private System.Windows.Forms.ColumnHeader RecievedHeader1;
        private System.Windows.Forms.ColumnHeader timeHeader;
        private System.Windows.Forms.ColumnHeader TimeHeader1;
        private System.Windows.Forms.ColumnHeader logTimeHeader;
        private System.Windows.Forms.TabPage NotifyMessages;
        private System.Windows.Forms.ListView NotifyMessageView;
        private System.Windows.Forms.ColumnHeader NotifyMessageHeader;
        private System.Windows.Forms.ColumnHeader NotifyRecievedHeader;
        private System.Windows.Forms.ColumnHeader NotifyTimeHeader;
        private System.Windows.Forms.TabPage AutomaticMessageTab;
        private System.Windows.Forms.ListView AutoMessageView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button hostSecureStart;
        private System.Windows.Forms.Button hostUSBstart;
        private System.Windows.Forms.Button USBScan;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSettingsToolStripMenuItem;
    }
}

