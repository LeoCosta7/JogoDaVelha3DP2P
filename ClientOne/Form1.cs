using ClientOne.DTO;
using ClientOne.DTO.Interface;
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
        Button symbolButton = new Button();
        GameMessage gameMessage;
        User user;
        string OpponentName, OpponentSymbol;
        Dictionary<string, bool> statusChangeItems;

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

            statusChangeItems = new Dictionary<string, bool>
            {
                {"btnTic",  false},
                {"Symbol" , false},
                {"SurrenderButton" , false},
                {"Send" , false},
                {"NewGameButton" , false},
            };

            ChangeButtonsStatus(statusChangeItems);
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {

            IPAddress ip = IPAddress.Parse(ServerIPtextBox.Text);
            int port = int.Parse(ServerPortTextBox.Text);

            await StartServerAsync(ip, port);

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

                    ChangeButtonsStatus(statusChangeItems = new Dictionary<string, bool> { { "Symbol", true } });

                    client = await listenerSocket.AcceptAsync();

                    ChatTextBox.AppendText("Connect to ClientTwo" + Environment.NewLine);
                    STR = new StreamReader(new NetworkStream(client));
                    STW = new StreamWriter(new NetworkStream(client));
                    STW.AutoFlush = true;

                    await ProcessReceivedMessagesAsync();
                }
                catch
                {
                    client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint endPoint = new IPEndPoint(ip, port);
                    await client.ConnectAsync(endPoint);

                    ChangeButtonsStatus(statusChangeItems = new Dictionary<string, bool> { { "Symbol", true } });

                    ChatTextBox.AppendText("Connect to ClientTwo" + Environment.NewLine);
                    STR = new StreamReader(new NetworkStream(client));
                    STW = new StreamWriter(new NetworkStream(client));
                    STW.AutoFlush = true;

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
            await STW.WriteLineAsync($"{message}");
            this.ChatTextBox.Invoke(new MethodInvoker(delegate ()
            {
                ChatTextBox.AppendText(user.Nickname + ": " + message + Environment.NewLine);
            }));

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
                        ProcessReceivedJsonMessage(recieve);
                    }
                    else
                    {
                        ProcessReceivedNonJsonMessage(recieve);
                    }

                }
                catch (IOException ex)      //exceção de E/S (entrada/saída)
                {
                    // A conexão foi interrompida
                    HandleDisconnection();
                    break; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ProcessReceivedJsonMessage(string jsonMessage)
        {
            JObject receivedJson = JObject.Parse(recieve);
            string messageType = receivedJson["MessageType"].ToString();

            switch (messageType)
            {
                case "GameMessage":
                    ProcessGameMessage(receivedJson);
                    break;
                case "UserMessage":
                    ProcessUserMessage(receivedJson);
                    break;
            }
        }

        private void ProcessReceivedNonJsonMessage(string message)
        {
            this.ChatTextBox.Invoke(new MethodInvoker(delegate ()
            {
                ChatTextBox.AppendText(OpponentName + ": " + message + Environment.NewLine);
            }));
            recieve = "";
        }

        private void ProcessGameMessage (JObject receivedMessage)
        {
            GameMessage jsonGameMessage = receivedMessage.ToObject<GameMessage>();

            switch (jsonGameMessage.Position)
            {
                case "SurrenderButton":
                    ReceivedSurrenderUser();
                    break;
                case "NewGameButton":
                    ReceivedNewGameUser();
                    break;
                default:
                    SetBoardPosition(jsonGameMessage.Position, jsonGameMessage.Text);
                    isValidMove = jsonGameMessage.ClientPlayed;
                    break;
            }
        }

        private void ProcessUserMessage (JObject jsonUserMessage)
        {
            User receiceMessage = JsonConvert.DeserializeObject<User>(recieve);

            OpponentName = receiceMessage.Nickname;
            OpponentSymbol = receiceMessage.ChosenSymbol;

            DisableButton(FindButtonByName(string.Concat("Symbol", OpponentSymbol)));
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

        private void HandleDisconnection()
        {
            // Fechar os fluxos e o socket
            STR.Close();
            STW.Close();
            client.Close();
            
            isValidMove = false;

            MessageBox.Show("Disconnected from the server.");

            statusChangeItems = new Dictionary<string, bool>
            {
                { "btnTic", false },
                { "Symbol" , false }
            };

            ChangeButtonsStatus(statusChangeItems);
            ResetButtonsToNewGame("", "btnTic");
        }

        private void DisableButton(Button button)
        {
            button.Enabled = false;
        }

        private void ChangeButtonsStatus(Dictionary<string, bool> listItems)
        {
            
            foreach(KeyValuePair<string, bool> item in listItems)
            {
                foreach (Control control in Controls)
                {
                    if (control is Button button && button.Name.Contains(item.Key))
                    {
                        button.Enabled = item.Value;
                        listItems.Remove(item.Key);
                    }                        
                }
            }            
        }

        private void ResetButtonsToNewGame(string text, string type)
        {
            foreach (Control control in Controls)
            {
                if (control is Button button && button.Name.Contains(type))
                {
                    button.BackColor = Color.White;
                    button.Text = text;
                }
            }
        }

        private void SetBoardPosition(string position, string text)
        {
            Button button = FindButtonByName(position);
            button.Text = OpponentSymbol;
            CheckScore();
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

        private async Task SendMoveAsync(IMessageType message)
        {
            if (message.MessageType == "GameMessage")
            {
                string jsonMessage = JsonConvert.SerializeObject(gameMessage);

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
            else if (message.MessageType == "UserMessage")
            {
                string jsonMessage = JsonConvert.SerializeObject(user);

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
        }

        private async void btnTic1_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic1.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic1",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic1);

                CheckScore();
            }
        }

        private async void btnTic2_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic2.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic2",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic2);

                CheckScore();

            }
        }

        private async void btnTic3_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic3.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic3",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic3);

                CheckScore();
            }
        }

        private async void btnTic4_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic4.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic4",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic4);

                CheckScore();
            }
        }

        private async void btnTic5_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic5.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic5",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic5);

                CheckScore();
            }
        }

        private async void btnTic6_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic6.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic6",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic6);

                CheckScore();
            }
        }

        private async void btnTic7_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic7.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic7",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic7);

                CheckScore();
            }
        }

        private async void btnTic8_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic8.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic8",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic8);

                CheckScore();
            }
        }

        private async void btnTic9_Click(object sender, EventArgs e)
        {
            if (isValidMove)
            {
                btnTic9.Text = user.ChosenSymbol;

                gameMessage = new GameMessage
                {
                    Position = "btnTic9",
                    Text = user.ChosenSymbol,
                    ClientPlayed = true
                };

                isValidMove = false;

                await SendMoveAsync(gameMessage);

                DisableButton(btnTic9);

                CheckScore();
            }
        }

        private async void Symbol_Click(object sender, EventArgs e)
        {
            symbolButton = (Button)sender;

            if (symbolButton == SymbolX || symbolButton == SymbolO)
            {
                user = new User
                {
                    Nickname = NickName.Text,
                    ChosenSymbol = symbolButton.Text
                };
            }

            if (ValidateUserNickName() && ValidateChosenSymbol())
            {
                statusChangeItems = new Dictionary< string, bool>
                {
                    {  "btnTic", true },
                    { "SurrenderButton" , true },
                    { "Send" , true },
                    { "Symbol" , false },
                };

                ChangeButtonsStatus(statusChangeItems);

                await SendMoveAsync(user);
            }
        }

        private bool ValidateUserNickName()
        {
            if (string.IsNullOrEmpty(NickName.Text))
            {
                MessageBox.Show("Insert Nickname");
                return false;
            }
            return true;
        }

        private bool ValidateChosenSymbol()
        {
            if (string.IsNullOrEmpty(symbolButton.Text))
            {
                MessageBox.Show("Pick a symbol");
                return false;
            }
            return true;
        }

        void CheckScore()
        {
            if (btnTic1.Text == user.ChosenSymbol && btnTic2.Text == user.ChosenSymbol && btnTic3.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic2, btnTic3, user.Nickname);
            }
            else if (btnTic4.Text == user.ChosenSymbol && btnTic5.Text == user.ChosenSymbol && btnTic6.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic4, btnTic5, btnTic6, user.Nickname);
            }
            else if (btnTic7.Text == user.ChosenSymbol && btnTic8.Text == user.ChosenSymbol && btnTic9.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic7, btnTic8, btnTic9, user.Nickname);
            }
            else if (btnTic1.Text == user.ChosenSymbol && btnTic4.Text == user.ChosenSymbol && btnTic7.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic4, btnTic7, user.Nickname);
            }
            else if (btnTic2.Text == user.ChosenSymbol && btnTic5.Text == user.ChosenSymbol && btnTic8.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic2, btnTic5, btnTic8, user.Nickname);
            }
            else if (btnTic3.Text == user.ChosenSymbol && btnTic6.Text == user.ChosenSymbol && btnTic9.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic3, btnTic6, btnTic9, user.Nickname);
            }
            else if (btnTic1.Text == user.ChosenSymbol && btnTic5.Text == user.ChosenSymbol && btnTic9.Text == user.ChosenSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic5, btnTic9, user.Nickname);
            }

            // Para o simbolo bola

            if (btnTic1.Text == OpponentSymbol && btnTic2.Text == OpponentSymbol && btnTic3.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic2, btnTic3, OpponentName);
            }
            else if (btnTic4.Text == OpponentSymbol && btnTic5.Text == OpponentSymbol && btnTic6.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic4, btnTic5, btnTic6, OpponentName);
            }
            else if (btnTic7.Text == OpponentSymbol && btnTic8.Text == OpponentSymbol && btnTic9.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic7, btnTic8, btnTic9, OpponentName);
            }
            else if (btnTic1.Text == OpponentSymbol && btnTic4.Text == OpponentSymbol && btnTic7.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic4, btnTic7, OpponentName);
            }
            else if (btnTic2.Text == OpponentSymbol && btnTic5.Text == OpponentSymbol && btnTic8.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic2, btnTic5, btnTic8, OpponentName);
            }
            else if (btnTic3.Text == OpponentSymbol && btnTic6.Text == OpponentSymbol && btnTic9.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic3, btnTic6, btnTic9, OpponentName);
            }
            else if (btnTic1.Text == OpponentSymbol && btnTic5.Text == OpponentSymbol && btnTic9.Text == OpponentSymbol)
            {
                MarkWinnerBoard(btnTic1, btnTic5, btnTic9, OpponentName);
            }
        }

        private void MarkWinnerBoard(Button btn1, Button btn2, Button btn3, string winner)
        {
            btn1.BackColor = Color.PowderBlue;
            btn2.BackColor = Color.PowderBlue;
            btn3.BackColor = Color.PowderBlue;

            statusChangeItems = new Dictionary< string, bool>
            {
                { "btnTic" , false },
                { "NewGameButton" , true },
                { "SurrenderButton" , false },
            };

            ChangeButtonsStatus(statusChangeItems);

            MessageBox.Show($"The winner is Player {winner}", "TixTacToe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void SurrenderButton_Click(object sender, EventArgs e)
        {
            gameMessage = new GameMessage
            {
                Position = "SurrenderButton"
            };

            statusChangeItems = new Dictionary< string, bool>
            {
                { "NewGameButton" , true },
                { "btnTic" , false },
                { "SurrenderButton" , false },
            };

            ChangeButtonsStatus(statusChangeItems);

            await SendMoveAsync(gameMessage);

            SendMessageAsync($"The winner is Player {OpponentName}");
        }

        private async void ReceivedSurrenderUser()
        {
            statusChangeItems = new Dictionary<string, bool>
            {
                { "NewGameButton", true },
                { "btnTic", false },
                { "SurrenderButton", false },
            };

            ChangeButtonsStatus(statusChangeItems);

            await SendMessageAsync($"The winner is Player {user.Nickname}");
        }

        private async void NewGameButton_Click(object sender, EventArgs e)
        {
            gameMessage = new GameMessage
            {
                Position = "NewGameButton"
            };

            await SendMoveAsync(gameMessage);

            statusChangeItems = new Dictionary< string, bool>
            {
                { "btnTic" , true },
                { "NewGameButton" , false },
            };

            ChangeButtonsStatus(statusChangeItems);
            ResetButtonsToNewGame("", "btnTic");

            isValidMove = true;
        }

        private void ReceivedNewGameUser()
        {
            statusChangeItems = new Dictionary< string, bool>
            {
                { "btnTic", true },
                { "NewGameButton", false },
            };

            ChangeButtonsStatus(statusChangeItems);
            ResetButtonsToNewGame("", "btnTic");            //Se receber um new game, então o atual tabuleiro vai para o mesmo status do outro cliente que enviou a requisição

            isValidMove = true;
        }
    }
}