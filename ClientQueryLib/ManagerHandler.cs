using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientQueryLib
{
    public class ManagerHandler : RemoteInterface
    {
        private ManagerFormInterface remoteManager;
        private System.Drawing.Color managerColour;
        private int usedSCHandler;


        public ManagerHandler(ManagerFormInterface remoteManager, System.Drawing.Color managerColour)
        {
            this.remoteManager = remoteManager;
            this.managerColour = managerColour;
        }
        public bool recievesNotify
        {
            get { return false; }
        }

        public bool isRunning
        {
            get { return true; }
        }

        public void send(string message)
        {
            return;
            //throw new NotImplementedException();
        }

        public System.Drawing.Color HandlerColor
        {
            get { return managerColour; }
        }

        public int getID()
        {
            return -1;
        }

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

        public void BeginClose()
        {
            //throw new NotImplementedException();
        }

    }
}
