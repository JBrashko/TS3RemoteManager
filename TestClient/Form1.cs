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

namespace ClientQueryMonitor
{
    public partial class RemoteManagerTest : Form
    {
        delegate void ListBoxCallback(string message, bool recieving);
        NetworkStuffTest TS3ClientStuff;
        List<RemoteHandlerTest> RemoteHandlers = new List<RemoteHandlerTest>();
        Thread listenThread;
        Thread netThread;
        public RemoteManagerTest()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            int conPort = 25740;//25639;
        String ipAddressText = ClientIPtext.Text;
        IPAddress IPaddress = IPAddress.Parse(ipAddressText);
        Socket connection = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        connection.Connect(IPaddress, conPort);
        if (!connection.Connected)
        {
            MessageBox.Show("Unable to establish connection");
                return;
        }
        TS3ClientStuff = new NetworkStuffTest(connection, this);
        netThread = new Thread(new ThreadStart(TS3ClientStuff.doStuff));
        netThread.Start();
        }
        public void addCQMessage(String message, bool recieving)
        {
            if (this.CQMessages.InvokeRequired)
            {
                ListBoxCallback d = new ListBoxCallback(addCQMessage);
                Object[] parameters = new Object[2];
                parameters[0] = message;
                parameters[1] = recieving;
                this.Invoke(d, parameters);
            }
            else
            {
                CQMessages.Items.Add(message);
            }
        }

        public void addLogMessage(String message, bool recieving)
        {
            if (this.LogBox.InvokeRequired)
            {
                ListBoxCallback d = new ListBoxCallback(addLogMessage);
                Object[] parameters = new Object[2];
                parameters[0] = message;
                parameters[1] = recieving;
                this.Invoke(d, parameters);
            }
            else
            {
                LogBox.Items.Add(message);
            }
        }

        private void RemoteManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            TS3ClientStuff.beginClose();
            
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
            Console.WriteLine("Started listening on port:" + port);
            AsyncCallback callback = new AsyncCallback(ListenCallback);
            listener.BeginAccept(callback, listener);

        }

        private void sndButton_Click(object sender, EventArgs e)
        {
            string command = sndBox.Text+'\r'+'\n';
            sendCommand(command);
            
        }
        private void sendCommand(String command)
        {
            int i = TS3ClientStuff.send(command);
            addCQMessage(command, false);
        }
        public void addCQCommand(String command, bool remote)
        {
            sendCommand(command);
        }
        public void ListenCallback(IAsyncResult result)
        {
            String verifyconnect = "TS3 Remote connected successfully" + Environment.NewLine + "selected schandlerid=2" + Environment.NewLine;
            Console.WriteLine("Remote connected");
            Socket listener = null;
            Socket handler = null;
            listener = (Socket)result.AsyncState;
            handler = listener.EndAccept(result);
            RemoteHandlerTest handle = new RemoteHandlerTest(handler, this);
            handle.send(verifyconnect);
            RemoteHandlers.Add(handle);
        }
    }

    public class RemoteHandlerTest
    {
        private Socket connection;
        private Socket handler;
        private int port;
        private RemoteManagerTest parent;
        private bool running;

        public RemoteHandlerTest(Socket _listenSocket, RemoteManagerTest _parent)
        {
            this.connection = _listenSocket;
            parent = _parent;
            running = true;
        }
        public void startListening()
        {
            int bytes = 0;
            Byte[] bytesReceived = new Byte[256];
            String test = "";
            String buffer = "";
            int messageNumber = 0;
            String[] CQCommands;
            String CQCommand;
            do{
                bytes = connection.Receive(bytesReceived, bytesReceived.Length, 0);
                buffer += Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                CQCommands = buffer.Split('\n');
                for (int i = 0; i < (CQCommands.Length - 1); i++)
                {
                    CQCommand = CQCommands[i].Trim();
                    messageNumber++;
                    parent.addCQCommand
                        (CQCommand, true);

                }
                buffer = CQCommands[CQCommands.Length - 1];

            }
            while ((bytes > 0) && (running));
        this.close();
        }
        private void close()
        {
            connection.Close();
        }
        public int send(string command)
        {
            byte[] buffer = new Byte[256];
            byte[] message = Encoding.UTF8.GetBytes(command);
            return connection.Send(message);
        }
    }
    public class NetworkStuffTest
    {
        private Socket connection;
        private TcpClient TS3client;
        private RemoteManagerTest parent;
        private bool running;
        public NetworkStuffTest(Socket _connection, RemoteManagerTest _parent)
        {
            running = true;
            connection = _connection;
            parent = _parent;
        }
        public void doStuff()
        {
            int bytes = 0;
            Byte[] bytesReceived = new Byte[256];
            String test = "";
            String buffer = "";
            String CQMessage = "";
            String[] CQMessages;
            int messageNumber = 0;
            do
            {
                bytes = connection.Receive(bytesReceived, bytesReceived.Length, 0);
                buffer += Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                CQMessages = buffer.Split('\n');
                for (int i = 0; i < (CQMessages.Length - 1); i++)
                {
                    CQMessage = CQMessages[i].Trim();
                    messageNumber++;
                    parent.addCQMessage(CQMessage,true);

                }
                buffer = CQMessages[CQMessages.Length - 1];

            }
            while ((bytes > 0) && (running));
            this.close();
        }
        public int send(string command)
        {
            byte[] buffer = new Byte[256];
            byte[] message = Encoding.UTF8.GetBytes(command);
            return connection.Send(message);
        }
        private void close()
        {
            connection.Close();
        }
        public void beginClose()
        {
            running = false;
            close();

        }
        public Socket getConnection()
        {
            return this.connection;
        }
    }
    
}
