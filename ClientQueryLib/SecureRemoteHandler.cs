using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ClientQueryLib
{
    public class SecureRemoteHandler : RemoteHandler
    {
        public SecureRemoteHandler(Socket _connection, ManagerFormInterface _parent, Color _handlerColor, int _ID) :base(_connection,_parent,_handlerColor,_ID)
        {
            
        }
    }
}
