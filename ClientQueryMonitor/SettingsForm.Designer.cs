namespace ClientQueryMonitor
{
    partial class SettingsForm
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
            this.IPbutton = new System.Windows.Forms.Button();
            this.IPbox = new System.Windows.Forms.TextBox();
            this.CertButton = new System.Windows.Forms.Button();
            this.CertLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IPbutton
            // 
            this.IPbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IPbutton.Location = new System.Drawing.Point(194, 13);
            this.IPbutton.Name = "IPbutton";
            this.IPbutton.Size = new System.Drawing.Size(75, 23);
            this.IPbutton.TabIndex = 0;
            this.IPbutton.Text = "Save";
            this.IPbutton.UseVisualStyleBackColor = true;
            this.IPbutton.Click += new System.EventHandler(this.IPbutton_Click);
            // 
            // IPbox
            // 
            this.IPbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IPbox.Location = new System.Drawing.Point(12, 12);
            this.IPbox.Name = "IPbox";
            this.IPbox.Size = new System.Drawing.Size(176, 20);
            this.IPbox.TabIndex = 1;
            // 
            // CertButton
            // 
            this.CertButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CertButton.Location = new System.Drawing.Point(194, 43);
            this.CertButton.Name = "CertButton";
            this.CertButton.Size = new System.Drawing.Size(75, 23);
            this.CertButton.TabIndex = 2;
            this.CertButton.Text = "Change";
            this.CertButton.UseVisualStyleBackColor = true;
            this.CertButton.Click += new System.EventHandler(this.CertButton_Click);
            // 
            // CertLabel
            // 
            this.CertLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CertLabel.AutoSize = true;
            this.CertLabel.Location = new System.Drawing.Point(13, 43);
            this.CertLabel.Name = "CertLabel";
            this.CertLabel.Size = new System.Drawing.Size(35, 13);
            this.CertLabel.TabIndex = 3;
            this.CertLabel.Text = "label1";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.CertLabel);
            this.Controls.Add(this.CertButton);
            this.Controls.Add(this.IPbox);
            this.Controls.Add(this.IPbutton);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button IPbutton;
        private System.Windows.Forms.TextBox IPbox;
        private System.Windows.Forms.Button CertButton;
        private System.Windows.Forms.Label CertLabel;
    }
}