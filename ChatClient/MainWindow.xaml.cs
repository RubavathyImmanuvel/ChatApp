using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;

namespace ChatClient
{
    public partial class MainWindow : Window
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;

        public MainWindow()
        {
            InitializeComponent();
            ConnectToServer();
        }

        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();
                ChatTextBox.AppendText("Connected to server...\n");

                receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not connect to server: " + ex.Message);
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageTextBox.Text))
            {
                string message = MessageTextBox.Text;
                byte[] buffer = Encoding.ASCII.GetBytes(message);
                stream.Write(buffer, 0, buffer.Length);
                MessageTextBox.Clear();
                ChatTextBox.AppendText("You: " + message + "\n");
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[1024];
            int byteCount;

            try
            {
                while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    Dispatcher.Invoke(() => ChatTextBox.AppendText("Friend: " + message + "\n"));
                }
            }
            catch (Exception)
            {
                Dispatcher.Invoke(() => ChatTextBox.AppendText("Disconnected from server...\n"));
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            receiveThread.Abort();
            stream?.Close();
            client?.Close();
            base.OnClosing(e);
        }
    }
}
