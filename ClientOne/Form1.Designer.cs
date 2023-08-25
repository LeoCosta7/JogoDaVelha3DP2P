namespace ClientOne
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MessageTextBox6 = new TextBox();
            ChatTextBox = new TextBox();
            NickName = new TextBox();
            ServerIPtextBox = new TextBox();
            ServerPortTextBox = new TextBox();
            StartButton = new Button();
            SendButton = new Button();
            btnTic1 = new Button();
            SymbolX = new Button();
            SymbolO = new Button();
            btnTic2 = new Button();
            btnTic3 = new Button();
            btnTic6 = new Button();
            btnTic5 = new Button();
            btnTic4 = new Button();
            btnTic9 = new Button();
            btnTic8 = new Button();
            btnTic7 = new Button();
            SurrenderButton = new Button();
            NewGameButton = new Button();
            SuspendLayout();
            // 
            // MessageTextBox6
            // 
            MessageTextBox6.Location = new Point(656, 318);
            MessageTextBox6.Multiline = true;
            MessageTextBox6.Name = "MessageTextBox6";
            MessageTextBox6.Size = new Size(298, 79);
            MessageTextBox6.TabIndex = 0;
            // 
            // ChatTextBox
            // 
            ChatTextBox.Location = new Point(656, 158);
            ChatTextBox.Multiline = true;
            ChatTextBox.Name = "ChatTextBox";
            ChatTextBox.Size = new Size(298, 132);
            ChatTextBox.TabIndex = 1;
            // 
            // NickName
            // 
            NickName.Location = new Point(12, 24);
            NickName.Multiline = true;
            NickName.Name = "NickName";
            NickName.Size = new Size(151, 28);
            NickName.TabIndex = 4;
            // 
            // ServerIPtextBox
            // 
            ServerIPtextBox.Location = new Point(656, 23);
            ServerIPtextBox.Multiline = true;
            ServerIPtextBox.Name = "ServerIPtextBox";
            ServerIPtextBox.Size = new Size(150, 28);
            ServerIPtextBox.TabIndex = 5;
            // 
            // ServerPortTextBox
            // 
            ServerPortTextBox.Location = new Point(812, 23);
            ServerPortTextBox.Multiline = true;
            ServerPortTextBox.Name = "ServerPortTextBox";
            ServerPortTextBox.Size = new Size(142, 28);
            ServerPortTextBox.TabIndex = 6;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(661, 70);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(293, 36);
            StartButton.TabIndex = 7;
            StartButton.Text = "START";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // SendButton
            // 
            SendButton.Location = new Point(960, 318);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(59, 79);
            SendButton.TabIndex = 8;
            SendButton.Text = "SEND";
            SendButton.UseVisualStyleBackColor = true;
            SendButton.Click += SendButton_Click;
            // 
            // btnTic1
            // 
            btnTic1.Location = new Point(25, 174);
            btnTic1.Name = "btnTic1";
            btnTic1.Size = new Size(68, 64);
            btnTic1.TabIndex = 9;
            btnTic1.Text = "button3";
            btnTic1.UseVisualStyleBackColor = true;
            btnTic1.Click += btnTic1_Click;
            // 
            // SymbolX
            // 
            SymbolX.Location = new Point(12, 58);
            SymbolX.Name = "SymbolX";
            SymbolX.Size = new Size(64, 55);
            SymbolX.TabIndex = 11;
            SymbolX.Text = "X";
            SymbolX.UseVisualStyleBackColor = true;
            SymbolX.Click += Symbol_Click;
            // 
            // SymbolO
            // 
            SymbolO.Location = new Point(99, 58);
            SymbolO.Name = "SymbolO";
            SymbolO.Size = new Size(64, 55);
            SymbolO.TabIndex = 12;
            SymbolO.Text = "O";
            SymbolO.UseVisualStyleBackColor = true;
            SymbolO.Click += Symbol_Click;
            // 
            // btnTic2
            // 
            btnTic2.Location = new Point(99, 174);
            btnTic2.Name = "btnTic2";
            btnTic2.Size = new Size(68, 64);
            btnTic2.TabIndex = 13;
            btnTic2.Text = "button3";
            btnTic2.UseVisualStyleBackColor = true;
            btnTic2.Click += btnTic2_Click;
            // 
            // btnTic3
            // 
            btnTic3.Location = new Point(173, 174);
            btnTic3.Name = "btnTic3";
            btnTic3.Size = new Size(68, 64);
            btnTic3.TabIndex = 14;
            btnTic3.Text = "button3";
            btnTic3.UseVisualStyleBackColor = true;
            btnTic3.Click += btnTic3_Click;
            // 
            // btnTic6
            // 
            btnTic6.Location = new Point(173, 244);
            btnTic6.Name = "btnTic6";
            btnTic6.Size = new Size(68, 64);
            btnTic6.TabIndex = 17;
            btnTic6.Text = "button3";
            btnTic6.UseVisualStyleBackColor = true;
            btnTic6.Click += btnTic6_Click;
            // 
            // btnTic5
            // 
            btnTic5.Location = new Point(99, 244);
            btnTic5.Name = "btnTic5";
            btnTic5.Size = new Size(68, 64);
            btnTic5.TabIndex = 16;
            btnTic5.Text = "button3";
            btnTic5.UseVisualStyleBackColor = true;
            btnTic5.Click += btnTic5_Click;
            // 
            // btnTic4
            // 
            btnTic4.Location = new Point(25, 244);
            btnTic4.Name = "btnTic4";
            btnTic4.Size = new Size(68, 64);
            btnTic4.TabIndex = 15;
            btnTic4.Text = "button3";
            btnTic4.UseVisualStyleBackColor = true;
            btnTic4.Click += btnTic4_Click;
            // 
            // btnTic9
            // 
            btnTic9.Location = new Point(173, 314);
            btnTic9.Name = "btnTic9";
            btnTic9.Size = new Size(68, 64);
            btnTic9.TabIndex = 20;
            btnTic9.Text = "button6";
            btnTic9.UseVisualStyleBackColor = true;
            btnTic9.Click += btnTic9_Click;
            // 
            // btnTic8
            // 
            btnTic8.Location = new Point(99, 314);
            btnTic8.Name = "btnTic8";
            btnTic8.Size = new Size(68, 64);
            btnTic8.TabIndex = 19;
            btnTic8.Text = "button3";
            btnTic8.UseVisualStyleBackColor = true;
            btnTic8.Click += btnTic8_Click;
            // 
            // btnTic7
            // 
            btnTic7.Location = new Point(25, 314);
            btnTic7.Name = "btnTic7";
            btnTic7.Size = new Size(68, 64);
            btnTic7.TabIndex = 18;
            btnTic7.Text = "button3";
            btnTic7.UseVisualStyleBackColor = true;
            btnTic7.Click += btnTic7_Click;
            // 
            // SurrenderButton
            // 
            SurrenderButton.Location = new Point(454, 61);
            SurrenderButton.Name = "SurrenderButton";
            SurrenderButton.Size = new Size(122, 97);
            SurrenderButton.TabIndex = 21;
            SurrenderButton.Text = "Surrender";
            SurrenderButton.UseVisualStyleBackColor = true;
            SurrenderButton.Click += SurrenderButton_Click;
            // 
            // NewGameButton
            // 
            NewGameButton.Location = new Point(469, 209);
            NewGameButton.Name = "NewGameButton";
            NewGameButton.Size = new Size(116, 109);
            NewGameButton.TabIndex = 22;
            NewGameButton.Text = "NewGame";
            NewGameButton.UseVisualStyleBackColor = true;
            NewGameButton.Click += NewGameButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1041, 716);
            Controls.Add(NewGameButton);
            Controls.Add(SurrenderButton);
            Controls.Add(btnTic9);
            Controls.Add(btnTic8);
            Controls.Add(btnTic7);
            Controls.Add(btnTic6);
            Controls.Add(btnTic5);
            Controls.Add(btnTic4);
            Controls.Add(btnTic3);
            Controls.Add(btnTic2);
            Controls.Add(SymbolO);
            Controls.Add(SymbolX);
            Controls.Add(btnTic1);
            Controls.Add(SendButton);
            Controls.Add(StartButton);
            Controls.Add(ServerPortTextBox);
            Controls.Add(ServerIPtextBox);
            Controls.Add(NickName);
            Controls.Add(ChatTextBox);
            Controls.Add(MessageTextBox6);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MessageTextBox6;
        private TextBox ChatTextBox;
        private TextBox NickName;
        private TextBox ServerIPtextBox;
        private TextBox ServerPortTextBox;
        private Button StartButton;
        private Button SendButton;
        private Button btnTic1;
        private Button btnTic2;
        private Button btnTic3;
        private Button btnTic4;
        private Button btnTic5;
        private Button btnTic6;
        private Button btnTic7;
        private Button btnTic8;
        private Button btnTic9;
        private Button SymbolX;
        private Button SymbolO;
        private Button SurrenderButton;
        private Button NewGameButton;
    }
}