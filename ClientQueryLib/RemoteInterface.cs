using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClientQueryLib
{
     public interface RemoteInterface
    {
        bool recievesNotify { get; }
        bool isRunning { get; }
        void send(String message);
        Color HandlerColor { get; }
        int getID();
        int UsedSCHandler { get; set; }
        void BeginClose();
    }
}
