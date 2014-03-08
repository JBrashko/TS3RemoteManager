using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientQueryLib
{
    public interface ManagerFormInterface
    {
         void addRecievedCQMessage(String message);
         void addLogMessage(String message, bool error);
         void ListenCallback(IAsyncResult result);
         void addHandledCQCommand(String command, RemoteInterface handler);
         void sendCQCommand(String command, RemoteInterface handler);
    }
}
