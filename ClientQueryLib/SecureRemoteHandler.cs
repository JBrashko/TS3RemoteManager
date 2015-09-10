using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClientQueryLib
{
    public class SecureRemoteHandler : RemoteHandler
    {
        private SslStream secureStream;
        public SecureRemoteHandler(Socket _connection, ManagerFormInterface _parent, Color _handlerColor, int _ID, X509Certificate2 cert) :base(_connection,_parent,_handlerColor,_ID)
        { //_connection.SocketType
            //stream = new SslStream()
            try {
                secureStream = new SslStream(this.netStream,false);
                //X509Certificate cert = parent.getCert();
                //secureStream.AuthenticateAsServer(cert,false,System.Security.Authentication.SslProtocols.Tls,false);
                stream = secureStream;
                String s = _connection.Connected.ToString();
                String p = _connection.RemoteEndPoint.ToString();
                this.send("Hello");
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Auth exception " + ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine("IO exception " + ex);
            }
            catch(NotSupportedException ex)
            {
                Console.WriteLine("Not supported exception " + ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Generic exception " + ex);
            }
        }
        protected override void processMessage(string command)
        {
            String a = command.ToString();
            base.processMessage(a);
        }
        public override void send(string command)
        {
            if (!running)
            {
                throw new HandlerException("Handler is not running");
            }
            try
            {
                byte[] buffer = new Byte[256];
                byte[] message = Encoding.UTF8.GetBytes(command.Trim() + '\r' + '\n');
                return;// connection.Send(message);
            }
            catch (SocketException ex)
            {
                this.BeginClose();
                throw new HandlerException(ex.Data.ToString());

            }
        }
        public override void ReadData()
        {
            try
            {
                int bytes = 0;
                Byte[] bytesReceived = new Byte[256];
                String buffer = "";
                int messageNumber = 0;
                String[] CQCommands;
                String CQCommand;
                do
                {
                    bytes = connection.Receive(bytesReceived, bytesReceived.Length, 0);
                    buffer += Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                    CQCommands = buffer.Split('\n');
                    for (int i = 0; i < (CQCommands.Length - 1); i++)
                    {
                        CQCommand = CQCommands[i].Trim();
                        messageNumber++;
                        processMessage(CQCommand);

                    }
                    buffer = CQCommands[CQCommands.Length - 1];

                }
                while ((bytes > 0) && (running));
                running = false;
                parent.addLogMessage(getName() + " connection has closed", false);
            }
            catch (SocketException ex)
            {
                running = false;
                parent.addLogMessage("A socket exception occoured in " + getName(), true);
            }
            finally
            {
                this.BeginClose();
            }
        }
    }
}
