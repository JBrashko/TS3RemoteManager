using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace ClientQueryLib
{
    public class TS3ClientHandler : Handler
    {
        private int initialisedState = 0;
        public int displayedSCHandler = 1;
        public TS3ClientHandler(Socket _connection, ManagerFormInterface _parent)
        {
            running = true;
            connection = _connection;
            parent = _parent;
        }
        protected override void processMessage(string message)
        {
            parent.addRecievedCQMessage(message);
            if (!initialised())
            {
                String compareString = getCompareString();

                if (initialisedState < 2 && message.Equals(compareString))
                {
                    initialisedState++;
                }
                else if (initialisedState == 2)
                {
                    Regex r = new Regex(compareString);
                    Match m = r.Match(message);
                    if (m.Success)
                    {
                        displayedSCHandler = Int32.Parse(m.Groups[1].ToString());
                        initialisedState++;
                        parent.addLogMessage("Successfully connected to TSClient", false);
                        parent.sendCQCommand("clientnotifyregister schandlerid=0 event=any", null);
                    }
                    else
                    {
                        String s = "Failed to match regex for init state 2";
                        parent.addLogMessage(s, true);
                    }
                }
                else if ((initialisedState == 3)&&(message.Equals(compareString)))
                {
                    initialisedState++;
                }
                else
                {
                    String a = "Invalid init state reached";
                    parent.addLogMessage(a, true);
                }
            }
        }
        public override string getName()
        {
            return "TS3 client connection";
        }
        private String getCompareString()
        {
            switch (initialisedState)
            {
                case 0:
                    return "TS3 Client";
                    break;
                case 1:
                    return "Welcome to the TeamSpeak 3 ClientQuery interface, type \"help\" for a list of commands and \"help <command>\" for information on a specific command.";
                case 2:
                    return "selected schandlerid=(\\d+)\\s*(.*)";
                case 3:
                    return "error id=0 msg=ok";
                default:
                    return "";
            }
        }
        private bool initialised()
        {
            return initialisedState > 2;
        }
        public Socket getConnection
        {
            get
            {
                return this.connection;
            }
        }
        public bool isInitialised
        {
            get
            {
                return initialised();
            }
        }

    }
}
