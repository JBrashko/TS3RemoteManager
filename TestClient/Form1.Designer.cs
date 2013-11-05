namespace ClientQueryMonitor
{
    partial class RemoteManagerTest
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
            this.ClientIPtext = new System.Windows.Forms.TextBox();
            this.CQMessages = new System.Windows.Forms.ListBox();
            this.hostStart = new System.Windows.Forms.Button();
            this.sndButton = new System.Windows.Forms.Button();
            this.sndBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LogBox = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(197, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ClientIPtext
            // 
            this.ClientIPtext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ClientIPtext.Location = new System.Drawing.Point(12, 15);
            this.ClientIPtext.Name = "ClientIPtext";
            this.ClientIPtext.Size = new System.Drawing.Size(179, 20);
            this.ClientIPtext.TabIndex = 1;
            this.ClientIPtext.Text = "127.0.0.1";
            // 
            // CQMessages
            // 
            this.CQMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CQMessages.FormattingEnabled = true;
            this.CQMessages.Location = new System.Drawing.Point(3, 3);
            this.CQMessages.Name = "CQMessages";
            this.CQMessages.Size = new System.Drawing.Size(246, 164);
            this.CQMessages.TabIndex = 2;
            // 
            // hostStart
            // 
            this.hostStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostStart.Location = new System.Drawing.Point(197, 41);
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
            this.sndButton.Location = new System.Drawing.Point(197, 272);
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
            this.sndBox.Location = new System.Drawing.Point(12, 274);
            this.sndBox.Name = "sndBox";
            this.sndBox.Size = new System.Drawing.Size(178, 20);
            this.sndBox.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 70);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(260, 196);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CQMessages);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(252, 170);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CQMessages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.LogBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(252, 170);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "RemoteLog";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogBox
            // 
            this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogBox.FormattingEnabled = true;
            this.LogBox.Location = new System.Drawing.Point(3, 3);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(246, 164);
            this.LogBox.TabIndex = 0;
            // 
            // RemoteManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 302);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.sndBox);
            this.Controls.Add(this.sndButton);
            this.Controls.Add(this.hostStart);
            this.Controls.Add(this.ClientIPtext);
            this.Controls.Add(this.ConnectButton);
            this.Name = "RemoteManager";
            this.Text = "CQRemoteManager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteManager_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox ClientIPtext;
        private System.Windows.Forms.ListBox CQMessages;
        private System.Windows.Forms.Button hostStart;
        private System.Windows.Forms.Button sndButton;
        private System.Windows.Forms.TextBox sndBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox LogBox;
    }
}

