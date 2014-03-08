using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LibUsbDotNet;

namespace ClientQueryLib
{
    class RemoteUSBHandler : RemoteInterface
    {

        private int id;
        private int usedSCHandler;
        private bool recievesnotify;
        private bool running;
        private Color handlerColor;
        private UsbDevice device;
        private ManagerFormInterface parent;
        public RemoteUSBHandler(ManagerFormInterface _parent, Color _handlerColor, int _ID)
        {
            parent = _parent;
            handlerColor = _handlerColor;
            id = _ID;
        }
        bool RemoteInterface.recievesNotify
        {
            get { return recievesnotify; }
        }

        bool RemoteInterface.isRunning
        {
            get { return running; }
        }

        int RemoteInterface.send(string message)
        {
            throw new NotImplementedException();
        }

        int RemoteInterface.getID()
        {
            return id;
        }
        public Color HandlerColor
        {
            get
            {
                return handlerColor;
            }
        }
        int RemoteInterface.UsedSCHandler
        {
            get
            {
                return usedSCHandler;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void RemoteInterface.BeginClose()
        {
            throw new NotImplementedException();
        }
    }
}
