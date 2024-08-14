using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace ChatServer
{
    public partial class MainWindow : Window
    {
        private TcpListener listener;
        private List<TcpClient> clients = new List<TcpClient>();

        public MainWindow()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            LogTextBox.AppendText("Server started...\n");

            Thread listenerThread = new Thread(ListenForClients);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);
                Dispatcher.Invoke(() => LogTextBox.AppendText("Client connected...\n"));

                Thread clientThread = new Thread(HandleClient);
                clientThread.IsBackground = true;
                clientThread.Start(client);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteCount;

            try
            {
                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    Dispatcher.Invoke(() => LogTextBox.AppendText("Received: " + message + "\n"));
                    BroadcastMessage(message, client);
                }
            }
            catch (Exception)
            {
                Dispatcher.Invoke(() => LogTextBox.AppendText("Client disconnected...\n"));
            }
            finally
            {
                clients.Remove(client);
                client.Close();
            }
        }

        private void BroadcastMessage(string message, TcpClient senderClient)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);

            foreach (TcpClient client in clients)
            {
                if (client != senderClient)
                {
                    try
                    {
                        NetworkStream stream = client.GetStream();
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch (Exception)
                    {
                        // Handle exception (optional)
                    }
                }
            }
        }
    }
}
