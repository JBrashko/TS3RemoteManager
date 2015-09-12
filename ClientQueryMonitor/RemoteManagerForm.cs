using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using System.Collections.ObjectModel;
using LibUsbDotNet.DeviceNotify;
using ClientQueryLib;
using System.Security.Cryptography.X509Certificates;

namespace ClientQueryMonitor
{
    public partial class RemoteManager : Form, ManagerFormInterface
    {
        delegate void LogCallback(string message, bool recieving);
        delegate void TabPageCallback(TabPage page);
        delegate void SpecificListViewCallback(ListViewItem item, ListView view);
        private IDeviceNotifier USBDeviceNotifier;
        private Color managerColour = Color.Coral;
        private Color automatedColor = Color.LightCoral;
        private Color notifyColor = Color.LightSkyBlue;
        private KeepAlive keepAlive;
        private RemoteInterface manager;
        private Host httpHost;
        private SecureHost shttpHost;
        private TS3ClientHandler TS3ClientStuff;
        private static X509Certificate2 serverCertificate = null;
        List<RemoteInterface> RemoteInterfaces = new List<RemoteInterface>();
        List<CommandLog> CommandHistory = new List<CommandLog>();
        int HistoryPosition = 0;
        Thread netThread;
        Regex notifyMessage = new Regex("(\\S+)\\sschandlerid=(\\d+)\\s*(.*)");
        Regex commandResponse = new Regex("error\\sid=(\\d+)\\smsg=([^$]*)");
        private bool closing = false;

        public RemoteManager()
        {
            InitializeComponent();
            manager = new ManagerHandler(this, managerColour);
            USBDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
            USBDeviceNotifier.Enabled = false;
            USBDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            TS3ClientConnect();
            hostSecureStart.Enabled = true;
            hostStart.Enabled = true;
            hostUSBstart.Enabled = true;
            ConnectButton.Enabled = false;
        }
        private void TS3ClientConnect()
        {
            int conPort = 25639;
            String ipAddressText = ClientQueryMonitor.Properties.Settings.Default.IP;
            IPAddress IPaddress = IPAddress.Parse(ipAddressText);
            addLogMessage("Connecting to TSClient at:" + ipAddressText + ":" + conPort, false);
            Socket connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            connection.Connect(IPaddress, conPort);
            if (!connection.Connected)
            {
                MessageBox.Show("Unable to establish connection");
                return;
            }
            TS3ClientStuff = new TS3ClientHandler(connection, this);
            netThread = new Thread(new ThreadStart(TS3ClientStuff.ReadData));
            netThread.Start();
            keepAlive = new KeepAlive(this);
            Thread keepAliveThread = new Thread(new ThreadStart(keepAlive.doStuff));
            keepAliveThread.Start();
        }
        private void addSentCQMessage(String message, RemoteInterface handler)
        {
            ListViewItem item = makeBaseMessageItem(message, false);
            if (handler == null)
            {
                item.BackColor = automatedColor;
            }
            else
            {
                item.BackColor = handler.HandlerColor;
            }
            addCQMessageToSpecificList(item, getListViewByHandler(handler));
            addCQMessageToList(item);

        }
        public void addRecievedCQMessage(String message)
        {
            ListViewItem item;
            if (!TS3ClientStuff.isInitialised)
            {
                item = makeRecievedMessageItem(message);
                item.BackColor = automatedColor;
                addCQMessageToSpecificList(item, getListViewByHandler(null));
                addCQMessageToList(item);
                return;
            }
            Match isNotify = notifyMessage.Match(message);
            if (isNotify.Success && !isNotify.Groups[1].ToString().Equals("selected"))
            {//this is a notify message
                item = makeNotifyMessageItem(message);
                addNotifyMessage(item);
                foreach (RemoteInterface remote in RemoteInterfaces)
                {
                    if (remote.recievesNotify && remote.isRunning)
                    {
                        remote.send(message);
                    }
                }
            }
            else
            {//this is a responce to a command
                item = makeRecievedMessageItem(message);
                String s = isNotify.Groups[1].ToString();
                bool usedSCHandlerChanged = s.Equals("selected");
                if (usedSCHandlerChanged)
                {
                    int n = Int32.Parse(isNotify.Groups[2].ToString());
                    TS3ClientStuff.UsedSCHandler = n;
                }
                if (CommandHistory.Count <= HistoryPosition)
                {
                    String stuff = "Invalid history position";
                    addLogMessage(stuff, true);
                    item.BackColor = Color.Yellow;
                }
                else
                {
                    CommandLog current = CommandHistory[HistoryPosition];
                    RemoteInterface usedHandler = current.Handler;
                    String command = current.Command;

                    if ((usedHandler != null))
                    {
                        item.BackColor = usedHandler.HandlerColor;
                        if (usedSCHandlerChanged)
                        {
                            usedHandler.UsedSCHandler = Int32.Parse(isNotify.Groups[2].ToString());
                        }
                        if (usedHandler.getID() != -1)
                        {
                            usedHandler.send(message);
                        }
                    }
                    else
                    {
                        item.BackColor = automatedColor;
                    }
                    addCQMessageToSpecificList(item, getListViewByHandler(usedHandler));
                    if (commandResponse.IsMatch(message))
                    {
                        HistoryPosition++;
                    }
                }
            }
            addCQMessageToList(item);
        }
        private ListViewItem makeRecievedMessageItem(String message)
        {
            ListViewItem item = makeBaseMessageItem(message, true);
            return item;
        }
        private ListViewItem makeNotifyMessageItem(String message)
        {
            ListViewItem item = makeBaseMessageItem(message, true);
            item.BackColor = notifyColor;
            return item;
        }
        private ListViewItem makeBaseMessageItem(String message, bool recieving)
        {
            ListViewItem item = new ListViewItem(message);
            item.SubItems.Add(recieving.ToString());
            item.SubItems.Add(DateTime.UtcNow.ToLongTimeString());
            return item;

        }

        private ListView getListViewByHandler(RemoteInterface handler)
        {
            if (handler == null)
            {
                return AutoMessageView;
            }
            if (handler.getID() == -1)
            {
                return managerCQMessages;
            }
            String pageName = "RemotePage" + handler.getID();
            if (MessageTabControl.TabPages.ContainsKey(pageName))
            {
                TabPage page = MessageTabControl.TabPages[MessageTabControl.TabPages.IndexOfKey("RemotePage" + handler.getID())];
                return (ListView)page.Controls[page.Controls.IndexOfKey("Remote" + handler.getID() + "CQMessages")];
            }

            return null;
        }
        private void addNotifyMessage(ListViewItem item)
        {
            addCQMessageToSpecificList(item, this.NotifyMessageView);
        }
        private void addCQMessageToList(ListViewItem item)
        {
            addCQMessageToSpecificList(item, this.MessageView);
        }
        private void addCQMessageToSpecificList(ListViewItem item, ListView currentView)
        {
            if ((currentView == null) || this.closing)
            {
                return;
            }

            if (currentView.InvokeRequired)
            {
                SpecificListViewCallback d = new SpecificListViewCallback(addCQMessageToSpecificList);
                Object[] parameters = new Object[2];
                parameters[0] = item;
                parameters[1] = currentView;
                this.Invoke(d, parameters);
            }
            else
            {
                currentView.Items.Add((ListViewItem)item.Clone());
            }
        }
        public void addLogMessage(String message, bool error)
        {
            if (closing)
            {
                return;
            }

            if (this.LogView.InvokeRequired)
            {
                LogCallback d = new LogCallback(addLogMessage);
                Object[] parameters = new Object[2];
                parameters[0] = message;
                parameters[1] = error;
                this.Invoke(d, parameters);
            }
            else
            {
                ListViewItem log = new ListViewItem(message);
                log.SubItems.Add(DateTime.UtcNow.ToLongTimeString());
                if (error)
                {
                    log.BackColor = Color.Yellow;
                }
                else
                {
                    log.BackColor = Color.LightGreen;
                }
                LogView.Items.Add(log);
                Console.WriteLine(message);
            }
        }

        private void hostStart_Click(object sender, EventArgs e)
        {
            httpHost = new Host(this);
            hostStart.Enabled = false;
        }

        private void sndButton_Click(object sender, EventArgs e)
        {
            ManagerCommandSend();
        }
        private void ManagerCommandSend()
        {
            string command = sndBox.Text;
            sndBox.Clear();
            sendCommand(command, manager);
        }
        private void sendCommand(String command, RemoteInterface handler)
        {
            if ((handler != null) && (handler.UsedSCHandler != TS3ClientStuff.UsedSCHandler))
            {
                sendCommand("use " + handler.UsedSCHandler, null);
            }
            addSentCQMessage(command, handler);
            CommandHistory.Add(new CommandLog(command, handler));
            try
            {
               TS3ClientStuff.send(command.Trim() + '\r' + '\n');
            }
            catch (HandlerException ex)
            {
                TS3ClientConnect();
                TS3ClientStuff.send(command.Trim() + '\r' + '\n');
            }
            keepAlive.addSleepTime();

        }
        public void sendCQCommand(String command, RemoteInterface handler)
        {
            sendCommand(command, handler);
        }
        public void addHandledCQCommand(String command, RemoteInterface handler)
        {

        }
        public X509Certificate2 getCert()
        {   if (serverCertificate ==null)
            {
                initialiseCert();
            }
            return serverCertificate;
        }
        private void initialiseCert()
        {
          serverCertificate = new X509Certificate2(ClientQueryMonitor.Properties.Settings.Default.CertificatePath,"Test123");
        }
        public void addHandler(Socket sock)
        {
            String verifyconnect = "TS3 Remote Manager" + Environment.NewLine + "TS3 remote connected successfully" + Environment.NewLine + "selected schandlerid=" + TS3ClientStuff.displayedSCHandler + Environment.NewLine;
            RemoteHandler handler = new RemoteHandler(sock, this, Color.Azure, RemoteInterfaces.Count);
            Thread handleThread = new Thread(new ThreadStart(handler.ReadData));
            handleThread.Name = handler.getName() + " thread";
            handleThread.Start();
            handler.send(verifyconnect);
            addTabPage(makeRemotePage(handler));
            RemoteInterfaces.Add(handler);
        }
        public void addSecureHandler(Socket sock)
        {
            String verifyconnect = "TS3 Remote Manager" + Environment.NewLine + "TS3 remote connected successfully" + Environment.NewLine + "selected schandlerid=" + TS3ClientStuff.displayedSCHandler + Environment.NewLine;
            SecureRemoteHandler handler = new SecureRemoteHandler(sock, this, Color.Azure, RemoteInterfaces.Count,getCert());
            Thread handleThread = new Thread(new ThreadStart(handler.ReadData));
            handleThread.Name = handler.getName()+" thread";
            handleThread.Start();
            handler.send(verifyconnect);
            addTabPage(makeRemotePage(handler));
            RemoteInterfaces.Add(handler);
        }
        private void addTabPage(TabPage page)
        {
            if (MessageTabControl.InvokeRequired)
            {
                TabPageCallback d = new TabPageCallback(addTabPage);
                Object[] parameters = new Object[1];
                parameters[0] = page;
                this.Invoke(d, parameters);
            }
            else
            {
                MessageTabControl.TabPages.Add(page);
            }
        }
        private TabPage makeRemotePage(RemoteHandler handler)
        {
            TabPage buildpage = new TabPage("Remote number " + handler.getID());
            ListView tempmessageview = new ListView();
            buildpage.Name = "RemotePage" + handler.getID();
            ColumnHeader tempMessageHeader = new ColumnHeader();
            tempMessageHeader.Name = "Message" + handler.getID().ToString();
            tempMessageHeader.Text = "Message";
            tempMessageHeader.Width = 276;
            ColumnHeader tempRecievedHeader = new ColumnHeader();
            tempRecievedHeader.Text = "Recieved";
            tempRecievedHeader.Width = 62;
            tempRecievedHeader.Name = "Recieved" + handler.getID().ToString();
            ColumnHeader tempTimeHeader = new ColumnHeader();
            tempTimeHeader.Text = "Time";
            tempTimeHeader.Width = 62;
            tempTimeHeader.Name = "Time" + handler.getID().ToString();
            tempmessageview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            tempMessageHeader,
            tempRecievedHeader,
            tempTimeHeader});
            tempmessageview.Dock = System.Windows.Forms.DockStyle.Fill;
            tempmessageview.GridLines = true;
            tempmessageview.Name = "Remote" + handler.getID() + "CQMessages";
            tempmessageview.View = System.Windows.Forms.View.Details;
            tempmessageview.Size = MessageView.Size;
            buildpage.Controls.Add(tempmessageview);
            return buildpage;
        }
        public void HandlerClose(Handler handler)
        {
            ListView a = getListViewByHandler((RemoteInterface)handler);
            TabPage p = (TabPage)a.Parent;

        }
        private void RemoteManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            closing = true;
            if ((keepAlive != null) && (keepAlive.Running))
            {
                keepAlive.Close();
            }
            if ((TS3ClientStuff != null) && (TS3ClientStuff.isRunning))
            {
                TS3ClientStuff.BeginClose();
            }
            foreach (RemoteInterface handler in RemoteInterfaces)
            {
                if (handler.isRunning)
                {
                    handler.BeginClose();
                }
            }
        }

        private void sndBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ('\r'.Equals(e.KeyChar))
            {
                ManagerCommandSend();
            }
        }

        private void hostSecureStart_Click(object sender, EventArgs e)
        {
            shttpHost = new SecureHost(this);
            hostSecureStart.Enabled = false;
        }

        private void hostUSBstart_Click(object sender, EventArgs e)
        {
            if (USBDeviceNotifier.Enabled)
            {
                USBDeviceNotifier.Enabled = false;
                hostUSBstart.Text = "Start USB host";
            }
            else
            {
                USBDeviceNotifier.Enabled = true;
                hostUSBstart.Text = "Stop USB host";
            }



        }
        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            LibUsbDotNet.DeviceNotify.Info.IUsbDeviceNotifyInfo deviceInfo = e.Device;
            switch (e.EventType)
            {
                case EventType.DeviceArrival:
                    break;
                case EventType.DeviceRemoveComplete:

                    break;
                case EventType.DeviceRemovePending:
                    break;
                default:
                    break;

            }
            addLogMessage(e.ToString(), false);
        }
        private void USBScan_Click(object sender, EventArgs e)
        {
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            UsbDeviceFinder finder = new UsbDeviceFinder(6473, 10, 216);//pid 10, vid 6473 rev 216
            UsbRegDeviceList a = allDevices.FindAll(finder);
            UsbDevice MyUsbDevice;
            String asdf = "";
            foreach (UsbRegistry device in a)
            {
                bool active = device.IsAlive;
                bool usable = device.Open(out MyUsbDevice);
                if (active && usable)
                {
                    asdf += MyUsbDevice.Info;
                }
            }
            String stuff = "asdf";
            /*foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out MyUsbDevice))
                {
                    addLogMessage(MyUsbDevice.Info.ToString(),false);
                    for (int iConfig = 0; iConfig < MyUsbDevice.Configs.Count; iConfig++)
                    {
                        UsbConfigInfo configInfo = MyUsbDevice.Configs[iConfig];
                        addLogMessage(configInfo.ToString(),false);

                        ReadOnlyCollection<UsbInterfaceInfo> interfaceList = configInfo.InterfaceInfoList;
                        for (int iInterface = 0; iInterface < interfaceList.Count; iInterface++)
                        {
                            UsbInterfaceInfo interfaceInfo = interfaceList[iInterface];
                            addLogMessage(interfaceInfo.ToString(),false);

                            ReadOnlyCollection<UsbEndpointInfo> endpointList = interfaceInfo.EndpointInfoList;
                            for (int iEndpoint = 0; iEndpoint < endpointList.Count; iEndpoint++)
                            {
                                addLogMessage(endpointList[iEndpoint].ToString(),false);
                            }
                        }
                    }
                }
            }*/
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void editSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm set = new SettingsForm();
            set.ShowDialog();
            
        }

        public int getDisplayedSCHandlerID()
        {
            return TS3ClientStuff.displayedSCHandler;
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
        }
    }
}

    
    