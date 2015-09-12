using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace ClientQueryLib
{
    public abstract class Handler
    {
        protected Stream stream;
        protected NetworkStream netStream;
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
        public virtual void ReadData()
        {
            try
            {
                byte[] myReadBuffer;
                StringBuilder builder;
                String buffer = "";
                String[] CQMessages;
                String CQCommand;
                String tmp;
                int bytes = 0;
                int messageNumber = 0;
                long pos = 0;
                do
                {
                    builder = new StringBuilder();
                    myReadBuffer = new byte[1024];
                    bytes = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                   // pos = stream.Position;
                    builder.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, bytes));
                    tmp = buffer + builder.ToString();
                    CQMessages = tmp.Split('\n');
                    for (int i = 0; i < (CQMessages.Length - 1); i++)
                    {
                        CQCommand = CQMessages[i].Trim();
                        messageNumber++;
                        processMessage(CQCommand);

                    }
                    buffer = CQMessages[CQMessages.Length - 1];
                }
                while ((bytes > 0) && (running));
                running = false;
                parent.addLogMessage(getName() + " connection has closed", false);
            }
            catch (System.IO.IOException ex)
            {
                running = false;
                parent.addLogMessage("An IO exception occoured in " + getName(), true);
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
        public abstract string getName();
        protected abstract void processMessage(string message);

        public virtual void send(string command) 
        {
            if (!running)
            {
                throw new HandlerException("Handler is not running");
                
            }
            try
            {
                byte[] buffer = new Byte[256];
                byte[] message = Encoding.UTF8.GetBytes(command.Trim() + '\r' + '\n');
                stream.Write(message, 0, message.Length);
                return;
            }
            catch (SocketException ex)
            {
                this.BeginClose();
                throw new HandlerException("A socket exception occoured",ex);

            }
            catch (HandlerException ex)
            {
                this.BeginClose();
                throw ex;

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
        Exception inner;
            public HandlerException(String _error)
            {
                error = _error;
            }
        public  HandlerException(String _error,Exception _inner)
        { error = _error;
            inner = _inner;
        }
        }
    
}
