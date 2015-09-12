using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClientQueryLib
{
    public class SecureRemoteHandler : RemoteHandler
    {
        private SslStream secureStream;
        public SecureRemoteHandler(Socket _connection, ManagerFormInterface _parent, Color _handlerColor, int _ID, X509Certificate2 cert) :base(_connection,_parent,_handlerColor,_ID)
        { 
            try {
                secureStream = new SslStream(this.netStream,false);
                secureStream.AuthenticateAsServer(cert,false,System.Security.Authentication.SslProtocols.Tls,false);
                stream = secureStream;
                String s = _connection.Connected.ToString();
                String p = _connection.RemoteEndPoint.ToString();
                Console.WriteLine("Cipher: {0} strength {1}", secureStream.CipherAlgorithm, secureStream.CipherStrength);
                Console.WriteLine("Hash: {0} strength {1}", secureStream.HashAlgorithm, secureStream.HashStrength);
                Console.WriteLine("Key exchange: {0} strength {1}", secureStream.KeyExchangeAlgorithm, secureStream.KeyExchangeStrength);
                Console.WriteLine("Protocol: {0}", secureStream.SslProtocol);
                Console.WriteLine("Is authenticated: {0} as server? {1}", secureStream.IsAuthenticated, secureStream.IsServer);
                Console.WriteLine("IsSigned: {0}", secureStream.IsSigned);
                Console.WriteLine("Is Encrypted: {0}", secureStream.IsEncrypted);
                Console.WriteLine("Can read: {0}, write {1}", secureStream.CanRead, secureStream.CanWrite);
                Console.WriteLine("Can timeout: {0}", secureStream.CanTimeout);
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Auth exception " + ex);
            }
            catch (IOException ex)
            {
                Console.WriteLine("IO exception " + ex);
            }
            catch(NotSupportedException ex)
            {
                Console.WriteLine("Not supported exception " + ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Generic exception " + ex);
            }
        }
        protected override void processMessage(string command)
        {
            String a = command.ToString();
            base.processMessage(a);
        }
    }
}
