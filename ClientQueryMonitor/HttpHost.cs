using ClientQueryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ClientQueryMonitor
{
    class HttpHost : RemoteHost
    {
        private RemoteManager manager;
        public HttpHost (RemoteManager _manager)
        {
            manager = _manager;
            hostStart();
        }
        private void hostStart()
        {
            SocketPermission permission = new SocketPermission(NetworkAccess.Accept, TransportType.Tcp, "", SocketPermission.AllPorts);
            permission.Demand();
            int port = 25740;//Int32.Parse(hstPort.Text);
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(20);
           manager.addLogMessage("Started listening on port:" + port, false);
            AsyncCallback callback = new AsyncCallback(ListenCallback);
            listener.BeginAccept(callback, listener);
            //hostStart.Enabled = false;
        }
        public void ListenCallback(IAsyncResult result)
        {
           
            manager.addLogMessage("Remote connected", false);
            Socket listener = null;
            Socket handlerSocket = null;
            listener = (Socket)result.AsyncState;
            handlerSocket = listener.EndAccept(result);
            listener.BeginAccept(new AsyncCallback(ListenCallback), listener);
            manager.addHandler(handlerSocket);
            /*RemoteHandler handler = new RemoteHandler(handlerSocket, this, Color.Azure, RemoteInterfaces.Count);
            Thread handleThread = new Thread(new ThreadStart(handler.ReadData));
            handleThread.Start();
            handler.send(verifyconnect);
            addTabPage(makeRemotePage(handler));
            RemoteInterfaces.Add(handler);*/
        }
    }
}
