using ClientQueryLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace ClientQueryMonitor
{
    class SecureHost : RemoteHost
    {
        private RemoteManager manager;
        private X509Certificate2 serverCertificate;

        public SecureHost (RemoteManager _manager)
        {
            manager = _manager;
            serverCertificate = manager.getCert();
            hostStart();
        }
        private void hostStart()
        {
            SocketPermission permission = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
            permission.Demand();
            int port = 25741;//Int32.Parse(hstPort.Text
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());// Dns.Resolve(Dns.GetHostName());
            AsyncCallback callback = new AsyncCallback(ListenCallback);
            foreach (IPAddress Address in ipHostInfo.AddressList)
            {
               // if(Address.AddressFamily== AddressFamily.InterNetwork)
              //  {
                IPEndPoint localEndPoint = new IPEndPoint(Address, port);
                Socket listener = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(20);
                listener.BeginAccept(callback, listener);
            //    }
            }
            IPHostEntry ent = Dns.GetHostEntry("localhost");
            foreach (IPAddress Address in ent.AddressList)
            {
                IPEndPoint localEndPoint = new IPEndPoint(Address, port);
                Socket listener = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(localEndPoint);
                listener.Listen(20);
                listener.BeginAccept(callback, listener);
            }
            manager.addLogMessage("Started listening on port:" + port, false);
            
            
            //hostStart.Enabled = false;
        }
        public void ListenCallback(IAsyncResult result)
        {
            manager.addLogMessage("Secure remote connected", false);
            Socket listener = null;
            Socket handlerSocket = null;
            listener = (Socket)result.AsyncState;
            handlerSocket = listener.EndAccept(result);
            listener.BeginAccept(new AsyncCallback(ListenCallback), listener);
            manager.addSecureHandler(handlerSocket);
            /*RemoteHandler handler = new RemoteHandler(handlerSocket, this, Color.Azure, RemoteInterfaces.Count);
            Thread handleThread = new Thread(new ThreadStart(handler.ReadData));
            handleThread.Start();
            handler.send(verifyconnect);
            addTabPage(makeRemotePage(handler));
            RemoteInterfaces.Add(handler);*/
        }
    }
}
