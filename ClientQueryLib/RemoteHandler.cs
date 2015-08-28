using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Drawing;

namespace ClientQueryLib
{
    public class RemoteHandler :Handler, RemoteInterface
    {
        protected bool recievenotify = false;
        protected Color handlerColor;     
        protected int ID;
        public RemoteHandler(Socket _connection, ManagerFormInterface _parent, Color _handlerColor, int _ID)
        {
            this.connection = _connection;
            parent = _parent;
            running = true;
            handlerColor = _handlerColor;
            ID = _ID;
        }
        public override string getName()
        {
            return "Remote handler id" + ID;
        }
        protected override void processMessage(String command)
        {
            if (!recievenotify&&command.Equals("clientnotifyregister schandlerid=0 event=any"))
            {
                recievenotify = true;
                this.send("error id=0 msg=ok");
                parent.addLogMessage(getName()+" registered for notifications", false);
                return;
            }
            if (command.Equals("currentschandlerid"))
            {
                this.send("schandlerid=2");
                this.send("error id=0 msg=ok");
                parent.addLogMessage("KeepAlive message recieved from " + getName(), false);
                return;
            }
            parent.sendCQCommand(command, this);           
        }
        public bool recievesNotify
        {
            get
            {
                return recievenotify;
            }
        }
        public Color HandlerColor
        {
            get
            {
                return handlerColor;
            }
        }
        public int getID()
        {
            return ID;
        }
        public bool isRunning
        {
            get
            {
                return running;
            }
        }


    }
    
}
