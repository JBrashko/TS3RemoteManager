using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClientQueryMonitor
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            IPbox.Text = ClientQueryMonitor.Properties.Settings.Default.IP;
            CertLabel.Text = ClientQueryMonitor.Properties.Settings.Default.CertificatePath;
        }

        private void IPbutton_Click(object sender, EventArgs e)
        {
            ClientQueryMonitor.Properties.Settings.Default.IP = IPbox.Text;
            ClientQueryMonitor.Properties.Settings.Default.Save();
        }

        private void CertButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.FileName = ClientQueryMonitor.Properties.Settings.Default.CertificatePath;
            diag.Filter = "Certificate file (*.cer)|*.cer";
            diag.InitialDirectory = ClientQueryMonitor.Properties.Settings.Default.CertificatePath;
            diag.CheckPathExists = true;
            diag.CheckFileExists = true;
            diag.Multiselect = false;
            DialogResult d = diag.ShowDialog();
            if(d==DialogResult.OK)
            {
                ClientQueryMonitor.Properties.Settings.Default.CertificatePath = diag.FileName;
                ClientQueryMonitor.Properties.Settings.Default.Save();
                CertLabel.Text = ClientQueryMonitor.Properties.Settings.Default.CertificatePath;

            }
        }
    }
}
