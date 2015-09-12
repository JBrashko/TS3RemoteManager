using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClientQueryLib
{
    public interface ManagerFormInterface
    {
         void addRecievedCQMessage(String message);
         void addLogMessage(String message, bool error);
         void addHandledCQCommand(String command, RemoteInterface handler);
         void sendCQCommand(String command, RemoteInterface handler);
         X509Certificate2 getCert();
         int getDisplayedSCHandlerID();
    }
}
