using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sever
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void tbnSend_Click(object sender, EventArgs e)
        {
            foreach(Socket item in clientList)
            {
                Send(item);
                AddMess(textMess.Text);
            }

            textMess.Clear();
            
        }
        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;
        void Connect()
        {
            clientList = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            server.Bind(IP);
            Thread Listen = new Thread(() => {
                try
                {
                    while (true)
                    {
                        server.Listen(100);
                        Socket client = server.Accept();
                        clientList.Add(client);

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch {
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                } 
            });
            Listen.IsBackground = true;
            Listen.Start();
        }
        void Close()
        { }
        void Send(Socket client)
        {
            if (client != null && textMess.Text != string.Empty)
                client.Send(Serialize(textMess.Text));
        }
        void Receive(object obj)
        {
            Socket client = obj as Socket;

            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);

                    string Mess = (string)Deserialize(data);

                    foreach(Socket item in clientList)
                    {
                        if(item !=null && item !=client)
                        item.Send(Serialize(Mess));
                    }

                    AddMess(Mess);
                }

            }
            catch
            {
                clientList.Remove(client);
                client.Close();
            }
        }
        void AddMess(string s)
        {
            lsvMess.Items.Add(new ListViewItem() { Text = s });
        }
        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, obj);
            return stream.ToArray();

        }
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();

            return formatter.Deserialize(stream);

        }

      


    }
}
