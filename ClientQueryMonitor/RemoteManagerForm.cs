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

namespace ClientQueryMonitor
{
    public partial class RemoteManager : Form
    {
        delegate void LogCallback(string message, bool recieving);
        delegate void TabPageCallback(TabPage page);
        delegate void SpecificListViewCallback(ListViewItem item, ListView view);
        private Color managerColour = Color.Coral;
        private Color automatedColor = Color.LightCoral;
        private Color notifyColor = Color.LightSkyBlue;
        private KeepAlive keepAlive;
        private RemoteHandler manager;
        TS3ClientHandler TS3ClientStuff;
        List<RemoteHandler> RemoteHandlers = new List<RemoteHandler>();
        List<CommandLog> CommandHistory = new List<CommandLog>();
        int HistoryPosition = 0;
        Thread netThread;
        Regex notifyMessage = new Regex("(\\S+)\\sschandlerid=(\\d+)\\s*(.*)");
        Regex commandResponse = new Regex("error\\sid=(\\d+)\\smsg=([^$]*)");
        private bool closing = false;
        
        public RemoteManager()
        {
            InitializeComponent();
            manager = new RemoteHandler(null, this, managerColour, -1);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            TS3ClientConnect();
        }
        private void TS3ClientConnect()
        {
            int conPort = 25639;
            String ipAddressText = ClientIPtext.Text;
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
        private void addSentCQMessage(String message,RemoteHandler handler)
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
        {   ListViewItem item;
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
                foreach (RemoteHandler handler in RemoteHandlers)
                {
                    if (handler.recievesNotify && handler.isRunning)
                    {
                        handler.send(message);
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
                    RemoteHandler usedHandler = current.Handler;
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
        /*private ListViewItem makeMessageItem(String message, bool recieving,RemoteHandler source)
        {
            ListViewItem item = new ListViewItem(message);
            item.SubItems.Add(recieving.ToString());
            item.SubItems.Add(DateTime.UtcNow.ToLongTimeString());
            if (!TS3ClientStuff.isInitialised)
            {
                item.BackColor = automatedColor;
                addCQMessageToSpecificList(item, getListViewByHandler(null));
                return item;
            }
            if (recieving)
            {
                Match isNotify = notifyMessage.Match(message);
                if (isNotify.Success && !isNotify.Groups[1].ToString().Equals("selected"))
                {//this is a notify message
                    String notifyEvent = isNotify.Groups[1].ToString();
                    if (notifyEvent.Equals("notifycurrentserverconnectionchanged"))
                    {
                        int n = Int32.Parse(isNotify.Groups[2].ToString());
                        TS3ClientStuff.displayedSCHandler = n;
                    }
                    item.BackColor = Color.LightSkyBlue;
                    addNotifyMessage(item);
                    foreach (RemoteHandler handler in RemoteHandlers)
                    {
                        if (handler.recievesNotify && handler.isRunning)
                        {
                            handler.send(message);
                        }
                    }

                }
                else
                {//this is a responce to a command
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
                        RemoteHandler usedHandler = current.Handler;
                        String command = current.Command;

                        if (usedHandler != null)
                        {
                            usedHandler.send(message);
                            item.BackColor = usedHandler.HandlerColor;
                            if (usedSCHandlerChanged)
                            {
                                usedHandler.UsedSCHandler = Int32.Parse(isNotify.Groups[2].ToString());
                            }
                        }
                        else
                        {
                            item.BackColor = managerColour;

                        }
                        addCQMessageToSpecificList(item, getListViewByHandler(usedHandler));
                        if (commandResponse.IsMatch(message))
                        {
                            HistoryPosition++;
                        }
                    }
                }
            }
            else
            {
                if (source != null)
                {
                    item.BackColor = source.HandlerColor;
                }
                else
                {
                    item.BackColor = managerColour;
                }
                addCQMessageToSpecificList(item, getListViewByHandler(source));
            }
            return item;
        }*/
        private ListView getListViewByHandler(RemoteHandler handler)
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
            if ((currentView == null)||this.closing)
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
            SocketPermission permission = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
            permission.Demand();
            int port = 25740;//Int32.Parse(hstPort.Text);
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            Socket listener = new Socket(ipAddress.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(20);
            addLogMessage("Started listening on port:" + port,false);
            AsyncCallback callback = new AsyncCallback(ListenCallback);
            listener.BeginAccept(callback, listener);   
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
        private void sendCommand(String command, RemoteHandler handler)
        {
            if ((handler != null)&&(handler.UsedSCHandler!=TS3ClientStuff.UsedSCHandler))
            {
                sendCommand("use " + handler.UsedSCHandler, null);
            }
            addSentCQMessage(command, handler);
            CommandHistory.Add(new CommandLog(command, handler));
            try
            {
                int i = TS3ClientStuff.send(command.Trim() + '\r' + '\n');
            }
            catch (HandlerException ex)
            {
                TS3ClientConnect();
                int i = TS3ClientStuff.send(command.Trim() + '\r' + '\n');
            }
            keepAlive.addSleepTime();
            
        }
        public void sendCQCommand(String command, RemoteHandler handler)
        {
            sendCommand(command,handler);
        }
        public void addHandledCQCommand(String command, RemoteHandler handler)
        {

        }
        public void ListenCallback(IAsyncResult result)
        {
            String verifyconnect = "TS3 Remote Manager"+Environment.NewLine+ "TS3 remote connected successfully" + Environment.NewLine + "selected schandlerid="+TS3ClientStuff.displayedSCHandler + Environment.NewLine;
            addLogMessage("Remote connected",false);
            Socket listener = null;
            Socket handlerSocket = null;
            listener = (Socket)result.AsyncState;
            handlerSocket = listener.EndAccept(result);
            listener.BeginAccept(new AsyncCallback(ListenCallback), listener);
            RemoteHandler handler = new RemoteHandler(handlerSocket, this,Color.Azure,RemoteHandlers.Count);
            Thread handleThread = new Thread(new ThreadStart(handler.ReadData));
            handleThread.Start();
            handler.send(verifyconnect);
            addTabPage(makeRemotePage(handler));
            RemoteHandlers.Add(handler);
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
            TabPage buildpage = new TabPage("Remote number "+handler.getID());
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
            foreach(RemoteHandler handler in RemoteHandlers)
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


    }

    public class KeepAlive
    {
        private RemoteManager parent;
        private bool running;
        private string command;
        private Thread thisthread;
        private int KeepAliveTime;
        public KeepAlive(RemoteManager _parent)
        {
            parent = _parent;
            running = true;
            command = "currentschandlerid";
            KeepAliveTime = 1000 * 60*5;
        }
        public void doStuff()
        {
            thisthread = Thread.CurrentThread;
            Thread.Sleep(10 * 1000);
            while (running)
            {
                
                try
                {
                    sendKeepAlive();

                }
                catch (Exception ex)
                {
                    parent.addLogMessage("Error sending KeepAlive message", true);
                }
                finally
                {
                    resetSleepTime();
                }
            }
        }
        private void sendKeepAlive()
        {
            parent.addLogMessage("Sending keep alive message", false);
            parent.sendCQCommand(command, null);
        }

        public void Close()
        {
            running = false;
            thisthread.Interrupt();
        }
        public void addSleepTime()
        {
            try
            {
                if (running && (thisthread != null) && (thisthread.ThreadState == ThreadState.Suspended))
                {
                    parent.addLogMessage("KeepAlive, Extending sleep time", false);
                    thisthread.Interrupt();
                }

            }
            catch (Exception ex)
            {
                parent.addLogMessage("KeepAlive, Error adding sleep time. ", false);
            }
        }
        private void resetSleepTime()
        {
            if (!running)
            {
                return;
            }
            try
            {
                Thread.Sleep(KeepAliveTime);
            }
            catch (ThreadInterruptedException ex)
            {
                parent.addLogMessage("keepAlive sleep event interrupted. ",false);
                resetSleepTime();
            }
        }
        public bool Running
        {
            get
            {
                return running;
            }

        }
    }
    public class CommandLog
    {
        public string Command { get
        {
            return command; 
        }}
        private string command = "";
        public RemoteHandler Handler
        {
            get
            {
                return handler;
            }

        }
        private RemoteHandler handler;
        public CommandLog(String _command, RemoteHandler _handler)
        {
            command = _command;
            handler=_handler;
        }
        public String toString()
        {
            return command;
        }

        


    }
}
