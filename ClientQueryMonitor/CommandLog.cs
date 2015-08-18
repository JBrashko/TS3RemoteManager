using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientQueryLib;

namespace ClientQueryMonitor
{
    public class CommandLog
    {
        public string Command
        {
            get
            {
                return command;
            }
        }
        private string command = "";
        public RemoteInterface Handler
        {
            get
            {
                return handler;
            }

        }
        private RemoteInterface handler;
        public CommandLog(String _command, RemoteInterface _handler)
        {
            command = _command;
            handler = _handler;
        }
        public String toString()
        {
            return command;
        }




    }
}
