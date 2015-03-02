using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;

namespace Saveyour
{
    public partial class Form1 : Form
    {
        static String SERVERIP = "127.0.0.1";
        static Int32 SERVERPORT = 80;
        static Boolean userClick = true;
        static Boolean passClick = true;

        public Form1()
        {
            InitializeComponent();
            //String response = Connect(SERVERIP, "Poop");
            button1.Click += new EventHandler(login_Click);
            textBox1.Click += new EventHandler(user_Click);
            textBox2.Click += new EventHandler(pass_Click);
            
        }

        /* Adding Listener for Log In Button */
        private void login_Click(object sender, System.EventArgs e)
        {
            String username = textBox1.Text;
            String password = textBox2.Text;
            String response = Connect(SERVERIP, username + "," + password);
            Debug.WriteLine(response); 
        }

        /* Adding Listener for Username Click */

        private void user_Click(object sender, System.EventArgs e)
        {   
            if (userClick)
            {
                textBox1.Text = String.Empty;
                userClick = false;
            }
        }

        /* Adding Listener for Password Click */

        private void pass_Click(object sender, System.EventArgs e)
        {
            if (passClick)
            {
                textBox2.Text = String.Empty;
                passClick = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {           
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        static String Connect(String server, String message)
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

                NetworkStream stream = client.GetStream();

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

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
            return "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
