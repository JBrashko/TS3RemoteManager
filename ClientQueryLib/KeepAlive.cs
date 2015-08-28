using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ClientQueryLib
{
    public class KeepAlive
    {
        private ManagerFormInterface parent;
        private bool running;
        private string command;
        private Thread thisthread;
        private int KeepAliveTime;
        public KeepAlive(ManagerFormInterface _parent)
        {
            parent = _parent;
            running = true;
            command = "currentschandlerid";
            KeepAliveTime = 1000 * 60 * 5;
        }
        public void doStuff()
        {
            thisthread = Thread.CurrentThread;
            thisthread.Name = "Keep alive thread";
            if (!running)
            {
                return;
            }
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
            if (thisthread.ThreadState != ThreadState.Stopped)
            {
                thisthread.Interrupt();
            }
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
                parent.addLogMessage("keepAlive sleep event interrupted. ", false);
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
}
