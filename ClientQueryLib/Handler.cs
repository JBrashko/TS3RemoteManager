using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ClientQueryLib
{
    public abstract class Handler
    {
        protected ManagerFormInterface parent;
        protected bool running;
        protected Socket connection;
        protected int usedSCHandler = 1;
        public int UsedSCHandler
        {
            get
            {
                return usedSCHandler;
            }
            set
            {
                usedSCHandler = value;
            }
        }
        public void ReadData()
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
                parent.addLogMessage(getName()+" connection has closed", false);
            }
            catch (SocketException ex)
            {
                running = false;
                parent.addLogMessage("A socket exception occoured in "+getName(), true);
            }
            finally
            {
                this.BeginClose();
            }
        }
        public abstract string getName();
        protected abstract void processMessage(string message);

        public int send(string command) 
        {
            if (!running)
            {
                throw new HandlerException("Handler is not running");
            }
            try
            {
                byte[] buffer = new Byte[256];
                byte[] message = Encoding.UTF8.GetBytes(command.Trim() + '\r' + '\n');
                return connection.Send(message);
            }
            catch (SocketException ex)
            {
                this.BeginClose();
                throw new HandlerException(ex.Data.ToString());

            }
        }
        public void BeginClose()
        {
            running = false;
            try
            {
                connection.Shutdown(SocketShutdown.Both);
                connection.BeginDisconnect(false, new AsyncCallback(CloseCallback), connection);
            }
            catch (SocketException ex)
            {

            }
            //connection.BeginDisconnect(false,)
        }
        private static void CloseCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);
            
        }
        public override string ToString()
        {
            return getName();
        }
        public bool isRunning
        {
            get
            {
                return running;
            }
        }
    }

        public class HandlerException : Exception
        {
            String error;
            public HandlerException(String _error)
            {
                error = _error;
            }
        }
    
}
