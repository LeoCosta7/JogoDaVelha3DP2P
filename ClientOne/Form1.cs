using ClientOne.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Sockets;

namespace ClientOne
{
    public partial class Form1 : Form
    {
        private Socket client;
        public StreamReader STR;
        public StreamWriter STW;
        public string recieve;
        public string TextToSend;
        private bool isValidMove = true;
        GameMessage moveMessage;

        public Form1()
        {
            InitializeComponent();

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerIPtextBox.Text = address.ToString();
                }
            }

            ChangeButtonsStatus(false);
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected)
            {
                Disconnect();
            }
            else
            {
                IPAddress ip = IPAddress.Parse(ServerIPtextBox.Text);
                int port = int.Parse(ServerPortTextBox.Text);

                await StartServerAsync(ip, port);
            }
        }

        private async Task StartServerAsync(IPAddress ip, int port)
        {
            try
            {
                try
                {
                    Socket listenerSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    listenerSocket.Bind(new IPEndPoint(ip, port));
                    listenerSocket.Listen(1);

                    client = await listenerSocket.AcceptAsync();

                    STR = new StreamReader(new NetworkStream(client));
                    STW = new StreamWriter(new NetworkStream(client));
                    STW.AutoFlush = true;

                    ChangeButtonsStatus(true);

                    await ProcessReceivedMessagesAsync();
                }
                catch
                {
                    client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(ip, port);
                    await client.ConnectAsync(endPoint);

                    STR = new StreamReader(new NetworkStream(client));
                    STW = new StreamWriter(new NetworkStream(client));
                    STW.AutoFlush = true;

                    ChangeButtonsStatus(true);

                    await ProcessReceivedMessagesAsync();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during server initialization: " + ex.Message);
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MessageTextBox6.Text))
            {
                TextToSend = MessageTextBox6.Text;
                await SendMessageAsync(TextToSend);
            }
            MessageTextBox6.Text = "";
        }

        private async Task SendMessageAsync(string message)
        {
            if (client.Connected)
            {
                await STW.WriteLineAsync($"{NickName.Text}: {message}");
                this.ChatTextBox.Invoke(new MethodInvoker(delegate ()
                {
                    ChatTextBox.AppendText(NickName.Text + ": " + message + Environment.NewLine);
                }));
            }
            else
            {
                MessageBox.Show("Sending Failed");
            }
        }

        private async Task ProcessReceivedMessagesAsync()
        {
            while (client.Connected)
            {
                try
                {
                    recieve = await STR.ReadLineAsync();

                    if (IsValidateJson(recieve))
                    {
                        GameMessage receivedMessage = JsonConvert.DeserializeObject<GameMessage>(recieve);

                        if (receivedMessage != null)
                        {
                            string position = receivedMessage.Position;
                            string text = receivedMessage.Text;
                            isValidMove = receivedMessage.ClientPlayed;

                            SetBoardPosition(position, text);
                        }
                    }
                    else
                    {
                        this.ChatTextBox.Invoke(new MethodInvoker(delegate ()
                        {
                            ChatTextBox.AppendText(recieve + Environment.NewLine);
                        }));
                        recieve = "";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private bool IsValidateJson(string input)
        {
            try
            {
                JToken.Parse(recieve);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Disconnect()
        {
            // Lógica para desconectar e redefinir a interface
            // ...
        }

        private void DisableButton(Button button)
        {
            button.Enabled = false;
        }

        private void ChangeButtonsStatus(bool status)
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name.Contains("btnTic"))
                    button.Enabled = status;
            }
        }

        private void SetBoardPosition(string position, string text)
        {
            Button button = FindButtonByName(position);

            button.Text = text;

            DisableButton(button);
        }

        private Button FindButtonByName(string position)
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name == position)
                {
                    return button;
                }
            }
            return null;
        }

        private async Task SendMoveAsync(GameMessage move)
        {
            string jsonMessage = JsonConvert.SerializeObject(move);

            if (client.Connected)
            {
                await STW.WriteLineAsync(jsonMessage);
                await STW.FlushAsync();
            }
            else
            {
                MessageBox.Show("Sending Failed");
            }
        }

        private async void btnTic1_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic1.Text = "X";

                moveMessage = new GameMessage
                {
                    Position = "btnTic1",
                    Text = "X",
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(moveMessage);

                DisableButton(btnTic1);
            }
        }

        private async void btnTic2_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic2.Text = "X";

                moveMessage = new GameMessage
                {
                    Position = "btnTic2",
                    Text = "X",
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(moveMessage);

                DisableButton(btnTic2);
            }
        }
    }
}