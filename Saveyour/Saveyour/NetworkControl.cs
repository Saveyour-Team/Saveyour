using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.IO;

namespace Saveyour
{
    class NetworkControl
    {
        String SERVERIP = "54.173.26.10";
        Int32 SERVERPORT = 1338;
        String certPath = @"ServerCertificate.pem";

        public String getIP()
        {
            return SERVERIP;
        }

        public static void addCertificate()
        {
            String certPath = @"ServerCertificate.pem";
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2 saveyour_certification = new X509Certificate2(certPath);
            store.Add(saveyour_certification);
            store.Close();
        }

        public String Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient. 
                // Note, for this client to work you need to have a TcpServer  
                // connected to the same address as specified by the server, port 
                // combination.

                TcpClient client = new TcpClient(server, SERVERPORT);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing. 
                //  Stream stream = client.GetStream();

                SslStream stream = new SslStream(client.GetStream());
                X509Certificate2Collection collection = new X509Certificate2Collection();
                collection.Import(certPath);

                stream.AuthenticateAsClient("SaveYour", collection, SslProtocols.Tls, false);

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response. 

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();

                return responseData;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
                return "";
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                return "";
            }
            catch (IOException e)
            {
                Console.WriteLine("IOException: {0}", e);
                return "";
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("AuthenticationException: {0}", e);
                return "Certificate";
            }
        }

    }
}
